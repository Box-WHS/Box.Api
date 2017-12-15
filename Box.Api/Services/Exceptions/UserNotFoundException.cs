using System;
using System.Runtime.Serialization;

namespace Box.Api.Services.Exceptions
{
    public class UserNotFoundException : ServiceException
    {
        public UserNotFoundException(Guid id) : base($"User with {id} not found")
        {
            
        }
        
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}