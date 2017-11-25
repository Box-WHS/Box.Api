using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json", "application/xml")]
    [Route("[controller]")]
    public class TrayController : Controller
    {
        
        
        [HttpGet("{boxId:long}")]
        public Task<IActionResult> GetTrays([FromQuery] long boxId)
        {
            return null;
        }
    }
}