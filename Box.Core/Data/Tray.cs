using System;
using System.Collections.Generic;

namespace Box.Core.Data
{
    public class Tray
    {
        /// <summary>
        ///     Id of the tray
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Referenced cards
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        ///     Execution interval
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        ///     Referenced Box
        /// </summary>
        public Box Box { get; set; }
    }
}