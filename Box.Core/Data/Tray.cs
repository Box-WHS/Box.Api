using System;
using System.Collections.Generic;

namespace Box.Core.Data
{
    /// <summary>
    ///     Represents a <see cref="Tray" /> which belongs to a <see cref="Box" /> and contains multiple <see cref="Cards" />
    /// </summary>
    public class Tray
    {
        public Tray()
        {
            Cards = new HashSet<Card>();
        }

        /// <summary>
        ///     Id of the tray
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Referenced cards
        /// </summary>
        public ICollection<Card> Cards { get; set; }

        /// <summary>
        ///     Execution interval
        /// </summary>
        public TimeSpan Interval { get; set; }
        
        /// <summary>
        /// Name of the <see cref="Tray"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Referenced Box
        /// </summary>
        public Box Box { get; set; }
    }
}