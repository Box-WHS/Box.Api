using System;
using System.Threading.Tasks;
using Box.Api.Data.DataContexts;
using Box.Api.Extensions;
using Box.Api.Services.Exceptions;
using Box.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Box.Api.Services
{
    public class ServiceBase
    {
        protected ServiceBase(BoxApiDataContext context)
        {
            Context = context;
        }

        protected BoxApiDataContext Context { get; private set; }

        protected async Task<User> GetUserById(Guid userGuid)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Guid == userGuid);

            ExceptionExtensions.ThrowIfNull(() => user, e => new UserNotFoundException(userGuid));
            return user;
        }
    }
}