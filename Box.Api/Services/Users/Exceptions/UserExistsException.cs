using System;
using System.Runtime.Serialization;
using Box.Api.Services.Users.Models;

namespace Box.Api.Services.Users.Exceptions
{
    [Serializable]
    public class UserExistsException : Exception
    {
        public UserExistsException( UserData user, Exception inner ) : base( $"The specified user with the id {user.Id} already exists", inner )
        {
            User = user;
        }

        protected UserExistsException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }

        public UserData User { get; set; }
    }
}