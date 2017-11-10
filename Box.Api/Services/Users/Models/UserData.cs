using System;
using System.ComponentModel.DataAnnotations;
using Box.Core.Validation;

namespace Box.Api.Services.Users.Models
{
    public class UserData
    {
        [ValidGuid]
        public Guid Id { get; set; }

        [Core.Validation.StringLength( 3, 32 )]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}