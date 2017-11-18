using System;

namespace Box.Core.Data
{
    public class Card
    {
        /// <summary>
        ///     Unique card Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Question which will be asked
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        ///     Answer to the question
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        ///     Timestamp when card was processed by user
        /// </summary>
        public DateTime LastProcessed { get; set; }

        /// <summary>
        ///     Referenced <see cref="Tray" />
        /// </summary>
        public Tray Tray { get; set; }

        /// <summary>
        ///     Concurrency token
        /// </summary>
        public Guid ConcurrencyToken { get; set; }
    }
}