using System;
using System.Collections.Generic;

namespace Box.Core.DataTransferObjects
{
    public class User
    {
        public User()
        {
            Boxes = new HashSet<Box>();
        }

        /// <summary>
        ///     User Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     A collection of <see cref="Box" />es which belong to the <see cref="User" />
        /// </summary>
        public ICollection<Box> Boxes { get; set; }

        /// <summary>
        ///     Concurrency token
        /// </summary>
        public Guid ConcurrencyToken { get; set; }

        /// <summary>
        ///     Safely adds a <see cref="Box" /> to the <see cref="User" />s <see cref="User.Boxes" />
        /// </summary>
        /// <param name="box"></param>
        public void AddBox( Box box )
        {
            Boxes.Add( box );
        }
    }
}