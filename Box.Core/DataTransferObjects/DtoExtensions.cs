
namespace Box.Core.DataTransferObjects
{
    public static class DtoExtensions
    {
        public static BoxDto ToBox(this Data.Box box)
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