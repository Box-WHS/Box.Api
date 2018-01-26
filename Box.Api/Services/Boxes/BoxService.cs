using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.Api.Controllers;
using Box.Api.Data.DataContexts;
using Box.Api.Extensions;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Boxes.Models;
using Box.Api.Services.Trays;
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
               //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .Where(b => b.User.Guid == userId)
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
               //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.User.Guid == userId && b.Id == boxId);

                ExceptionExtensions.ThrowIfNull(() => box, e => new BoxNotFoundException(boxId, e));

                return box.ToBoxDto();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BoxDto>> GetBoxes(Guid userId)
        {
            using (Context)
            {
               //var user = await GetUserById(userId);
                var boxes = await Context.Boxes
                    .AsNoTracking()
                    .Include(b => b.User)
                    .Where(b => b.User.Guid == userId)
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
               //var user = await GetUserById(userId);
                var boxes = await Context.Boxes
                    .Where(b => b.User.Guid == userId)
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

        public async Task<IEnumerable<TrayDto>> GetTrays(Guid userId, long boxId)
        {
            using (Context)
            {
               //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .AsNoTracking()
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.User.Guid == userId);

                var trays = await Context.Trays
                    .AsNoTracking()
                    .Include(t => t.Box)
                    .Include(t => t.User)
                    .Where(t => t.Box == box)
                    .ToListAsync();

                return trays.ConvertAll(t => t.ToTrayDto());
            }
        }

        public async Task<TrayDto> AddTray(Guid userId, long boxId, TrayCreationData creationData)
        {
            using (Context)
            {
               //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.User.Guid == userId);

                var tray = new Tray
                {
                    Box = box,
                    User = box.User,
                    Name = creationData.Name
                };

                var newTray = await Context.Trays.AddAsync(tray);
                await Context.SaveChangesAsync();

                return newTray.Entity.ToTrayDto();
            }
        }

        public async Task<TrayDto> GetTray(Guid userId, long boxId, long trayId)
        {
            using (Context)
            {
               //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.User.Guid == userId);

                var tray = await Context.Trays
                    .Where(t => t.Box == box && t.Id == trayId)
                    .FirstOrDefaultAsync();

                return tray.ToTrayDto();
            }
        }

        public async Task<IEnumerable<CardDto>> GetCards(Guid userId, long boxId, long trayId)
        {
            using (Context)
            {
               //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.User.Guid == userId);

                var tray = await Context.Trays
                    .Where(t => t.Box == box && t.Id == trayId)
                    .FirstOrDefaultAsync();

                var cards = await Context.Cards
                    .Include(c => c.Tray)
                    .Where(c => c.Tray == tray)
                    .ToListAsync();

                return cards.ConvertAll(c => c.ToCardDto());
            }
        }

        public async Task<CardDto> GetCard(Guid userId, long boxId, long trayId, long cardId)
        {
            using (Context)
            {
                //var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.User.Guid == userId);

                var tray = await Context.Trays
                    .Where(t => t.Box == box && t.Id == trayId)
                    .FirstOrDefaultAsync();

                var card = await Context.Cards
                    .Include(c => c.Tray)
                    .FirstOrDefaultAsync(c => c.Tray == tray && c.Id == cardId);

                return card.ToCardDto();
            }
        }

        public async Task<CardDto> AddCard(Guid userId, long boxId, long trayId, CardCreationData data)
        {
            using (Context)
            {
               ////var user = await GetUserById(userId);
                var box = await Context.Boxes
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.User.Guid == userId);

                var tray = await Context.Trays
                    .Where(t => t.Box == box && t.Id == trayId)
                    .FirstOrDefaultAsync();

                var card = new Card
                {
                    Answer = data.Answer,
                    Question = data.Question,
                    Tray = tray,
                    User = box.User
                };

                var newCard = await Context.Cards.AddAsync(card);
                await Context.SaveChangesAsync();
                
                return newCard.Entity.ToCardDto();
            }
        }
    }
}