using System;
using System.Threading.Tasks;
using Box.Api.Data.DataContexts;
using Box.Api.Services.Users.Exceptions;
using Box.Api.Services.Users.Models;
using Box.Core.DataTransferObjects;
using Box.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Box.Api.Services.Users
{
    [Service( typeof( IUserService ), ServiceLifetime.Scoped )]
    public class UserService : IUserService
    {
        public UserService( BoxApiDataContext context, ILogger<IUserService> logger )
        {
            Logger = logger;
            Context = context;
        }

        private ILogger<IUserService> Logger { get; }

        private BoxApiDataContext Context { get; }

        /// <inheritdoc />
        public async Task<Core.Data.User> AddUserAsync( UserData userData )
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
                    return addedUser.Entity.ToUser();
                }
            }
            catch ( DbUpdateException e )
            {
                // On duplicate key throw exception
                if ( e.InnerException is MySqlException mySqlException && mySqlException.Number == 1062 ) 
                {
                    Logger.LogError( $"User id {userData.Id} already exists" );
                    throw new UserExistsException( userData, e.InnerException );
                }
                Logger.LogError( $"Exception thrown while trying to add a new user with the id {userData.Id}, exception: {e}" );
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<Core.Data.User> GetUserAsync( Guid userId )
        {
            try
            {
                using ( Context )
                {
                    var user = await Context.Users.Include( u => u.Boxes )
                        .FirstOrDefaultAsync( u => u.Id == userId );

                    if ( user == null )
                    {
                        Logger.LogError( $"User with id {userId} does not exist" );
                        throw new UserNotFoundException( userId );
                    }

                    return user.ToUser();
                }
            }
            catch ( Exception e )
            {
                Logger.LogError( $"Exception thrown while trying to get a user with id {userId}, exception: {e}" );
                throw new UserNotFoundException( userId, e );
            }
        }

        /// <inheritdoc />
        public async Task<Core.Data.User> DeleteUserAsync( Guid userId )
        {
            try
            {
                using ( Context )
                {
                    var user = await Context.FindAsync<User>( userId );
                    Context.Remove( user );
                    Context.SaveChanges();
                    return user.ToUser();
                }
            }
            catch ( Exception e )
            {
                Logger.LogError( $"Failed to delete user with id {userId}, exception: {e}" );
                throw;
            }
        }
    }
}