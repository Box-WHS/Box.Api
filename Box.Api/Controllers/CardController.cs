using System.Linq;
using System.Threading.Tasks;
using Box.Api.Data.DataContexts;
using Box.Api.Services.Boxes;
using Box.Api.Services.Cards;
using Box.Api.Services.Trays;
using Box.Core.Data;
using Box.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CardController : Controller
    {
        private BoxApiDataContext DataContext;

        public CardController(BoxApiDataContext dataContext)
        {
            DataContext = dataContext;
        }

        [Route("{cardId:long}/move/{trayId:long}")]
        [HttpPut]
        public async Task<IActionResult> Move([FromRoute] long trayId, [FromRoute] long cardId, long boxId)
        {
            var card = await DataContext.Cards
                .Where(c => c.Id == cardId && c.Tray.User.Guid == User.GetId())
                .FirstOrDefaultAsync();
            var tray = await DataContext.Trays
                .Where(t => t.Id == trayId && t.User.Guid == User.GetId())
                .FirstOrDefaultAsync();

            if (card == null || tray == null)
                return NotFound();

            card.Tray = tray;
            await DataContext.SaveChangesAsync();
            return Ok();
        }
    }
}