using System;
using System.Collections.Generic;

namespace Box.Api.Types
{
    public class Fold
    {
        public int IdFold { get; set; }                // Primary-Key (UQ) for the fold
        public int Position { get; set; }              // Position of the fold inside the box
        public List<Card> Cards { get; set; }          // Cards inside the fold
        public TimeSpan Interval { get; set; }         // Interval to ask the cards
        public Box Box { get; set; }                   // Referenced Box
    }
}