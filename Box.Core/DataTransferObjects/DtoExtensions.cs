
using Box.Core.DataTransferObjects;

namespace Box.Core.Data
{
    public static class DtoExtensions
    {
        public static BoxDto ToBox(this Box box)
        {
            return new BoxDto
            {
                Id = box.Id,
                Name = box.Name,
                Trays = box.Trays
            };
        }
    }
}