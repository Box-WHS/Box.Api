using System;
using System.Runtime.Serialization;

namespace Box.Api.Services.Trays.Exceptions
{
    public class TrayHandlingException : Exception
    {
        public long TrayId { get; }


        public TrayHandlingException(long trayId)
        {
            TrayId = trayId;
        }

        public TrayHandlingException(string message, long trayId) : base(message)
        {
            TrayId = trayId;
        }

        public TrayHandlingException(string message, Exception innerException, long trayId) : base(
            message,
            innerException)
        {
            TrayId = trayId;
        }

        protected TrayHandlingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}