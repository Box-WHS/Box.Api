using System;
using System.Runtime.Serialization;

namespace Box.Api.Services.Trays.Exceptions
{
    public class TrayNotFoundException
        : TrayHandlingException
    {
        public static int NoTrayId = -1;

        private static string ErrorMessage(long trayId)
        {
            if (trayId != NoTrayId)
            {
                return $"Tray with Id {trayId} could not be found.";
            }
            return $"There is no Box which contains a tray.";
        }

        public TrayNotFoundException() : this(-1)
        {
        }

        public TrayNotFoundException(long trayId) : base(trayId)
        {
        }

        public TrayNotFoundException(long trayId, Exception innerException) : base(
            ErrorMessage(trayId),
            innerException,
            trayId)
        {
        }

        public TrayNotFoundException(string message, Exception innerException, long trayId) : base(
            ErrorMessage(trayId),
            innerException,
            trayId)
        {
        }

        protected TrayNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}