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
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Box.Api.Services.Boxes
{
    [Service(typeof(IBoxService), ServiceLifetime.Scoped)]
    public class BoxService : ServiceBase, IBoxService
    {
        /// <inheritdoc />
        public BoxService(ILogger<BoxService> logger, BoxApiDataContext context) : base(context)
        {
            Logger = logger;
        }

        private ILogger Logger { get; }

        /// <inheritdoc />
        public async Task<BoxDto> AddBox(Guid userId, BoxCreationData data)
        {
            using (Context)
            {
                var box = new Core.Data.Box
                {
                    User = await GetUserById(userId),
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
                var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .Where(b => b.User == user)
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
                var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.User == user && b.Id == boxId);

                ExceptionExtensions.ThrowIfNull(() => box, e => new BoxNotFoundException(boxId, e));

                return box.ToBoxDto();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BoxDto>> GetBoxes(Guid userId)
        {
            using (Context)
            {
                var user = await GetUserById(userId);
                var boxes = await Context.Boxes
                    .AsNoTracking()
                    .Include(b => b.User)
                    .Where(b => b.User == user)
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
                var user = await GetUserById(userId);
                var boxes = await Context.Boxes
                    .Where(b => b.User == user)
                    .ToListAsync();

                ExceptionExtensions.ThrowIfNull(() => boxes, e => new BoxNotFoundException(0, e));

                Context.RemoveRange(boxes);
                await Context.SaveChangesAsync();

                return boxes.ConvertAll(b => b.ToBoxDto());
            }
        }

        public async Task<User> AddUser(Guid userId)
        {
            using (Context)
            {
                var user = new User
                {
                    Guid = userId
                };

                var newUser = await Context.AddAsync(user);
                await Context.SaveChangesAsync();
                return newUser.Entity;
            }
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await GetUserById(userId);
        }

    }
}