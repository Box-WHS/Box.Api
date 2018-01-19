using System;

namespace Box.Core.DataTransferObjects
{
    /// <summary>
    ///     Represents a <see cref="TrayDto" /> which belongs to a <see cref="Box" /> and contains multiple <see cref="Cards" />
    /// </summary>
    public class TrayDto
    {
        /// <summary>
        /// Name of the <see cref="TrayDto"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Id of the tray
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Execution interval
        /// </summary>
        public TimeSpan Interval { get; set; }
        
        /// <summary>
        /// Box Id where the <see cref="TrayDto"/> belongs to
        /// </summary>
        public long BoxId { get; set; }
    }
}