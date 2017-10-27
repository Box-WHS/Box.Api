using System;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    [Produces( "application/json", "application/xml" )]
    [Route( "[controller]" )]
    public class BoxController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Boxes()
        {
            return Ok();
        }

        /// <summary>
        ///     Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete( "{id}" )]
        public IActionResult Delete( long id )
        {
            if ( DateTime.Now.Ticks % 2 == 0 )
            {
                return NotFound();
            }
            return new NoContentResult();
        }
    }
}