using System.ComponentModel.DataAnnotations;

namespace Box.Api.Services.Boxes.Models
{
    public class BoxChangeName
    {
        [Required]
        public long Id { get; set; }

        [Core.Validation.StringLength(3, 32)]
        public string NewName { get; set; }
    }
}