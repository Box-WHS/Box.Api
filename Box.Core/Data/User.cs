using System;

namespace Box.Core.Data
{
    public class User
    {
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
    }
}