using System;
using System.Threading.Tasks;
using Box.Api.Data.DataContexts;
using Box.Api.Services.Users.Exceptions;
using Box.Api.Services.Users.Models;
using Box.Core.Data;
using Box.Core.Services;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Box.Api.Services.Users
{
    [Service( typeof( IUserService ), ServiceLifetime.Scoped )]
    public class UserService : IUserService
    {
        public UserService( BoxApiDataContext context )
        {
            Context = context;
        }

        private BoxApiDataContext Context { get; }

        /// <inheritdoc />
        public async Task<User> AddUser( UserData userData )
        {
            try
            {
                using ( Context )
                {
                    var user = new User
                    {
                        Id = userData.Id,
                        Username = userData.Name,
                        Email = userData.Email
                    };

                    var addedUser = await Context.AddAsync( user );
                    Context.SaveChanges();
                    return addedUser.Entity;
                }
            }
            catch ( DbUpdateException e )
            {
                if ( e.InnerException is MySqlException mySqlException && mySqlException.Number == 1062 )
                {
                    throw new UserExistsException( userData, e.InnerException );
                }
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<User> GetUser( Guid userId )
        {
            try
            {
                using ( Context )
                {
                    var user = await Context.FindAsync<User>( userId );

                    if ( user == null )
                    {
                        throw new UserNotFoundException( userId );
                    }

                    return user;
                }
            }
            catch ( Exception e )
            {
                throw new UserNotFoundException( userId, e );
            }
        }
    }
}