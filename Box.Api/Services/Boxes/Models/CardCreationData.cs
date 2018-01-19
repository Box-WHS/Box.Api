using System.ComponentModel.DataAnnotations;

namespace Box.Api.Services.Boxes.Models
{
    public class CardCreationData
    {
        [Core.Validation.StringLength(4, 4000)]
        public string  Answer { get; set; }
        
        [Core.Validation.StringLength(4, 4000)]
        public string Question { get; set; }
    }
}