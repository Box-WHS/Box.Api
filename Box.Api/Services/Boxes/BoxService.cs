using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.Api.Controllers;
using Box.Api.Data.DataContexts;
using Box.Api.Extensions;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Boxes.Models;
using Box.Core.Data;
using Box.Core.DataTransferObjects;
using Box.Core.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Box.Api.Services.Boxes
{
    [Service(typeof(IBoxService), ServiceLifetime.Scoped)]
    public class BoxService : IBoxService
    {
        /// <inheritdoc />
        public BoxService(ILogger<BoxService> logger, BoxApiDataContext context)
        {
            Logger = logger;
            Context = context;
        }

        private ILogger Logger { get; }

        private BoxApiDataContext Context { get; }
        
        /// <inheritdoc />
        public async Task<BoxDto> AddBox(Guid userId, BoxCreationData data)
        {
            using (Context)
            {
                var box = new Core.Data.Box
                {
                    UserId = userId,
                    Name = data.Name
                };

                var result = await Context.AddAsync(box);
                await Context.SaveChangesAsync();

                return result.Entity.ToBoxDto();
            }
        }

        /// <inheritdoc />
        public async Task<BoxDto> ChangeName(Guid userId, BoxChangeName data)
        {
            using (Context)
            {
                //var user = await Context.FindAsync<User>( userId );
                var box = await Context.Boxes
                    .Where(b => b.Id == data.Id && b.UserId == userId)
                    .FirstOrDefaultAsync();

                ExceptionExtensions.ThrowIfNull(() => box, e => new BoxNotFoundException(data.Id));

                box.Name = data.NewName;
                await Context.SaveChangesAsync();
                return box.ToBoxDto();
            }
        }

        /// <inheritdoc/>
        public async Task<BoxDto> GetBox(Guid userId, long boxId)
        {
            using (Context)
            {
                    var box = await Context.Boxes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(b => b.UserId == userId && b.Id == boxId);

                ExceptionExtensions.ThrowIfNull(()=> box,e => new BoxNotFoundException(boxId, e));

                return box.ToBoxDto();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BoxDto>> GetBoxes(Guid userId)
        {
            using (Context)
            {
                var boxes = await Context.Boxes.AsNoTracking()
                    .Where(b => b.UserId == userId)
                    .ToListAsync();

                ExceptionExtensions.ThrowIfNull(() => boxes, e => new BoxNotFoundException(0, e));

                return boxes.ConvertAll(b => b.ToBoxDto());
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BoxDto>> DeleteBoxes(Guid userId)
        {
            using (Context)
            {
                var boxes = await Context.Boxes
                    .Where(b => b.UserId == userId)
                    .ToListAsync();

                ExceptionExtensions.ThrowIfNull(() => boxes, e => new BoxNotFoundException(0, e));

                Context.RemoveRange(boxes);
                await Context.SaveChangesAsync();

                return boxes.ConvertAll(b => b.ToBoxDto());
            }
        }
    }
}