using Box.Core.Validation;

namespace Box.Api.Controllers
{
    public class BoxCreationData
    {
        [StringLength(3, 32)]
        public string Name { get; set; }
    }
}