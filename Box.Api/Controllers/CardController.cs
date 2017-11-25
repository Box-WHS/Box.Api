using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json", "application/xml")]
    [Route("[controller]")]
    public class CardController : Controller
    {
        
    }
}