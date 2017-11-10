using System;
using System.Threading.Tasks;
using Box.Api.Services.Users.Exceptions;
using Box.Api.Services.Users.Models;
using Box.Core.DataTransferObjects;

namespace Box.Api.Services.Users
{
    public interface IUserService
    {
        /// <summary>
        ///     Adds an <see cref="User" /> to the database
        /// </summary>
        /// <param name="userData">     User data which will be used to create the user </param>
        /// <exception cref="UserExistsException">Will be thrown, if user id is already taken</exception>
        /// <returns>     Returns the created user </returns>
        Task<Core.Data.User> AddUserAsync( UserData userData );

        /// <summary>
        ///     Retreives a <see cref="User" /> from the database
        /// </summary>
        /// <param name="userId">User id of the user</param>
        /// <exception cref="UserNotFoundException">Will be thrown, if the user does not exist</exception>
        /// <returns>Returns the stored user</returns>
        Task<Core.Data.User> GetUserAsync( Guid userId );

        /// <summary>
        ///     Deletes the user with the given user id
        /// </summary>
        /// <param name="userId">User id of the user</param>
        /// <returns>The deleted user</returns>
        Task<Core.Data.User> DeleteUserAsync( Guid userId );
    }
}