using System;
using System.Runtime.Serialization;

namespace Box.Api.Services.Users.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException( Guid userId ) : base( CreateMessage( userId ) )
        {
        }

        public UserNotFoundException( Guid userId, Exception inner ) : base( CreateMessage( userId ), inner )
        {
        }

        protected UserNotFoundException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }

        private static string CreateMessage( Guid userId )
        {
            return $"User with id: {userId}, could not be retreived";
        }
    }
}