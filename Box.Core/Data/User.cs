using System;
using System.Collections.Generic;

namespace Box.Core.Data
{
    public class User
    {
        public User()
        {
            Boxes = new HashSet<Box>();
            Trays = new HashSet<Tray>();
            Cards = new HashSet<Card>();
        }

        public Guid Guid { get; set; }

        public long Id { get; set; }

        public ICollection<Box> Boxes { get; set; }

        public ICollection<Tray> Trays { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}