using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Box.Api.Controllers;
using Box.Api.Data.DataContexts;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Users;
using Box.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Box.Api.Services.Boxes
{
    public class BoxChangeName
    {
        [Required]
        public long Id { get; set; }

        [Core.Validation.StringLength(3, 32)]
        public string NewName { get; set; }
    }

    [Service(typeof(IBoxService), ServiceLifetime.Scoped)]
    public class BoxService : IBoxService
    {
        /// <inheritdoc />
        public BoxService(ILogger<BoxService> logger, BoxApiDataContext context, IUserService userService)
        {
            Logger = logger;
            Context = context;
            UserService = userService;
        }

        private ILogger Logger { get; }

        private BoxApiDataContext Context { get; }
        public IUserService UserService { get; }


        /// <inheritdoc />
        public async Task<Core.DataTransferObjects.Box> AddBox(Guid userId, BoxCreationData data)
        {
            using (Context)
            {
                var user = await Context.Users.Include(c => c.Boxes)
                    .FirstOrDefaultAsync(c => c.Id == userId);

                var box = new Core.DataTransferObjects.Box
                {
                    User = user,
                    Name = data.Name
                };
                user.AddBox(box);

                Context.Entry(user).State = EntityState.Modified;
                var result = await Context.AddAsync(box);
                await Context.SaveChangesAsync();

                return result.Entity;
            }
        }

        /// <inheritdoc />
        public async Task<Core.DataTransferObjects.Box> ChangeName(Guid userId, BoxChangeName data)
        {
            using (Context)
            {
                //var user = await Context.FindAsync<User>( userId );
                var box = await Context.Boxes
                    .Where(b => b.Id == data.Id && b.UserId == userId)
                    .FirstOrDefaultAsync();

                if (box == null)
                {
                    throw new BoxNotFoundException(data.Id);
                }

                box.Name = data.NewName;
                await Context.SaveChangesAsync();
                return box;
            }
        }

        /// <inheritdoc/>
        public async Task<Core.DataTransferObjects.Box> GetBox(Guid userId, long boxId)
        {
            using (Context)
            {
                var box = await Context.Boxes.AsNoTracking()
                    .FirstOrDefaultAsync(b => b.UserId == userId && b.Id == boxId);

                if (box == null)
                {
                    throw new BoxNotFoundException(boxId);
                }

                return box;
            }
        }
    }
}