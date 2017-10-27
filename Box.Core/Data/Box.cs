using System.Collections.Generic;

namespace Box.Core.Data
{
    public class Box
    {
        /// <summary>
        ///     Cardbox ID
        /// </summary>
        public long BoxId { get; set; }

        /// <summary>
        ///     Name of the Box
        /// </summary>
        public string BoxName { get; set; }

        /// <summary>
        ///     Trays inside the box
        /// </summary>
        public List<Tray> Trays { get; set; }
    }
}