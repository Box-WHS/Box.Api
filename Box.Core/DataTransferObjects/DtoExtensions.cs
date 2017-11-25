
using Box.Core.Data;

namespace Box.Core.DataTransferObjects
{
    public static class DtoExtensions
    {
        public static BoxDto ToBoxDto(this Data.Box box)
        {
            return new BoxDto
            {
                Id = box.Id,
                Name = box.Name,
                Trays = box.Trays
            };
        }

        public static TrayDto ToTrayDto(this Tray tray)
        {
            return new TrayDto
            {
                BoxId = tray.Box.Id,
                Id = tray.Id,
                Interval = tray.Interval
            };
        }

        public static CardDto ToCardDto(this Card card)
        {
            return new CardDto
            {
                Answer = card.Answer,
                Id = card.Id,
                LastProcessed = card.LastProcessed,
                Question = card.Question,
                Tray = card.Tray.Id
            };
        }
    }
}