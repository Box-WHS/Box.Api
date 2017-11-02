using System;
using System.Collections.Generic;

namespace Box.Core.Data
{
    public class Box
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
        public List<Tray> Trays { get; set; }

        /// <summary>
        ///     User Id
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Concurrency Toke
        /// </summary>
        public Guid ConcurrencyToken { get; set; }
    }
}