using System;
using System.ComponentModel.DataAnnotations;

namespace Box.Api.Services.Trays
{
    public class TrayCreationData
    {
        [Core.Validation.StringLength(3, 64)]
        public string Name { get; set; }

        [Required]
        public TimeSpan Interval { get; set; }
    }
}