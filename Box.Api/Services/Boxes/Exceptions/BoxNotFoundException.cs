using System;

namespace Box.Api.Services.Boxes.Exceptions
{
    public class BoxNotFoundException : Exception
    {
        public long Id { get; }
        
        public BoxNotFoundException(long id)
        {
            Id = id;
        }
    }
}