using System.Collections.Generic;
using Box.Core.Data;

namespace Box.Core.DataTransferObjects
{
    public class BoxDto
    {
        /// <summary>
        ///     Cardbox ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Name of the Box
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Trays inside the box
        /// </summary>
        public ICollection<Tray> Trays { get; set; }
    }
}