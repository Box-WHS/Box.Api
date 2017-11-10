using System;
using System.Collections.Generic;

namespace Box.Core.DataTransferObjects
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

        /// <summary>
        ///     <see cref="User" />Id of the owner
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     <see cref="User" /> which owns this <see cref="Box" />
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Concurrency token
        /// </summary>
        public Guid ConcurrencyToken { get; set; }
    }
}