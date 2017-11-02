using System;
using System.Collections.Generic;

namespace Box.Core.Data
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Email { get; set; }

        public List<Box> Boxes { get; set; }
    }
}