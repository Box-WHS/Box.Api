using System;
using System.Collections.Generic;

namespace Box.Core.Data
{
    public class Box
    {
        public Box()
        {
            Trays = new HashSet<Tray>();
        }

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

        public User User { get; set; }
    }
}