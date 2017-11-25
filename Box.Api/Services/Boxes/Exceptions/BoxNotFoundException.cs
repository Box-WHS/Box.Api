using System;
using System.Runtime.Serialization;

namespace Box.Api.Services.Boxes.Exceptions
{
    public class BoxNotFoundException : Exception
    {
        public long Id { get; }
        
        public BoxNotFoundException(long id)
        {
            Id = id;
        }

        public BoxNotFoundException(long id, Exception inner) : base("", inner)
        {
            Id = id;
        }

        public BoxNotFoundException(string message, long id) : base(message)
        {
            Id = id;
        }

        public BoxNotFoundException(string message, Exception innerException, long id) : base(message, innerException)
        {
            Id = id;
        }

        protected BoxNotFoundException(SerializationInfo info, StreamingContext context, long id) : base(info, context)
        {
            Id = id;
        }
    }
}