using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.Api.Data.DataContexts;
using Box.Api.Extensions;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Trays.Exceptions;
using Box.Core.Data;
using Box.Core.DataTransferObjects;
using Box.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Box.Api.Services.Trays
{
    [Service(typeof(ITrayService))]
    public class TrayService : ITrayService
    {
        private readonly BoxApiDataContext _apiDataContext;

        public TrayService(BoxApiDataContext apiDataContext)
        {
            _apiDataContext = apiDataContext;
        }

        public async Task<TrayDto> GetTray(Guid userId, long trayId)
        {
            using (_apiDataContext)
            {
                var tray = await _apiDataContext.Trays
                    .AsNoTracking()
                    .Where(t=>t.Id == trayId)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(() => tray, e => new TrayNotFoundException(trayId, e));

                return tray.ToTrayDto();
            }
        }

        public async Task<IEnumerable<TrayDto>> GetTrays(Guid userId, long boxId)
        {
            using (_apiDataContext)
            {
                var trays = await _apiDataContext.Trays
                    .AsNoTracking()
                    .Include(t => t.Box)
                    .Where(t => t.Box.Id == boxId)
                    .ToListAsync();

                ExceptionExtensions.ThrowIfNull(
                    () => trays,
                    e => new TrayNotFoundException(TrayNotFoundException.NoTrayId, e));

                return trays.ConvertAll(t => t.ToTrayDto());
            }
        }

        public async Task<TrayDto> AddTray(Guid userId, long boxId, TrayCreationData data)
        {
            using (_apiDataContext)
            {
                var box = await _apiDataContext.Boxes
                    .Where(b => b.Id == boxId)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(
                    () => box,
                    e => new TrayNotFoundException(TrayNotFoundException.NoTrayId, e));

                var tray = new Tray
                {
                    Box = box,
                    Interval = data.Interval,
                    Name = data.Name
                };

                var newTray = await _apiDataContext.AddAsync(tray);
                await _apiDataContext.SaveChangesAsync();
                return newTray.Entity.ToTrayDto();
            }
        }

        public async Task<TrayDto> DeleteTray(Guid userId, long boxId, long trayId)
        {
            using (_apiDataContext)
            {
                var tray = await _apiDataContext.Trays
                    .Include(t => t.Box)
                    .Where(t => t.Box.Id == boxId && t.Id == trayId)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(
                    () => tray,
                    e => new TrayNotFoundException(trayId, e));

                _apiDataContext.Remove(tray);
                await _apiDataContext.SaveChangesAsync();
                return tray.ToTrayDto();
            }
        }

        public async Task<TrayDto> RenameTray(Guid userId, long boxId, long trayId, string newName)
        {
            ExceptionExtensions.ThrowIfNullOrEmpty(() => newName);

            using (_apiDataContext)
            {
                var tray = await _apiDataContext.Trays
                    .AsTracking()
                    .Include(t => t.Box)
                    .Where(t => t.Box.Id == boxId && t.Id == trayId)
                    .FirstOrDefaultAsync();


                ExceptionExtensions.ThrowIfNull(() => tray, e => new TrayNotFoundException(trayId, e));

                tray.Name = newName;
                await _apiDataContext.SaveChangesAsync();
                return tray.ToTrayDto();
            }
        }
    }
}