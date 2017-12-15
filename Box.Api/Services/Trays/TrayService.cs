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
    public class TrayService : ServiceBase, ITrayService
    {
        public TrayService(BoxApiDataContext apiDataContext): base(apiDataContext)
        {
        }

        public async Task<TrayDto> GetTray(Guid userId, long trayId)
        {
            using (Context)
            {
                var user = await GetUserById(userId);
                var tray = await Context.Trays
                    .AsNoTracking()
                    .Include(t=> t.User)
                    .Where(t=>t.Id == trayId && t.User == user)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(() => tray, e => new TrayNotFoundException(trayId, e));

                return tray.ToTrayDto();
            }
        }

        public async Task<IEnumerable<TrayDto>> GetTrays(Guid userId, long boxId)
        {
            using (Context)
            {
                var trays = await Context.Trays
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
            using (Context)
            {
                var user = await GetUserById(userId);

                var box = await Context.Boxes
                    .Include(b => b.User)
                    .Where(b => b.Id == boxId && b.User == user)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(
                    () => box,
                    e => new TrayNotFoundException(TrayNotFoundException.NoTrayId, e));

                var tray = new Tray
                {
                    Box = box,
                    Interval = data.Interval,
                    Name = data.Name,
                    User = user
                };

                var newTray = await Context.AddAsync(tray);
                await Context.SaveChangesAsync();
                return newTray.Entity.ToTrayDto();
            }
        }

        public async Task<TrayDto> DeleteTray(Guid userId, long trayId)
        {
            using (Context)
            {
                var user = await GetUserById(userId);
                var tray = await Context.Trays
                    .Include(t => t.User)
                    .Where(t => t.Id == trayId && t.User == user)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(
                    () => tray,
                    e => new TrayNotFoundException(trayId, e));

                Context.Remove(tray);
                await Context.SaveChangesAsync();
                return tray.ToTrayDto();
            }
        }

        public async Task<TrayDto> RenameTray(Guid userId, long trayId, string newName)
        {
            ExceptionExtensions.ThrowIfNullOrEmpty(() => newName);

            using (Context)
            {
                var user = await GetUserById(userId);
                var tray = await Context.Trays
                    .AsTracking()
                    .Include(t => t.User)
                    .Where(t => t.Id == trayId && t.User == user)
                    .FirstOrDefaultAsync();


                ExceptionExtensions.ThrowIfNull(() => tray, e => new TrayNotFoundException(trayId, e));

                tray.Name = newName;
                await Context.SaveChangesAsync();
                return tray.ToTrayDto();
            }
        }
    }
}