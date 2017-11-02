using System;
using System.Net;
using System.Threading.Tasks;
using Box.Api.Services.Users;
using Box.Api.Services.Users.Exceptions;
using Box.Api.Services.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    [Produces( "application/json", "application/xml" )]
    [Route( "[controller]" )]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService )
        {
            _userService = userService;
        }

        [HttpGet( "{userId:guid}" )]
        public async Task<IActionResult> Get( [FromRoute] Guid userId )
        {
            try
            {
                var user = await _userService.GetUser( userId );
                return Ok( user );
            }
            catch ( Exception )
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] UserData userData )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            try
            {
                var user = await _userService.AddUser( userData );
                return CreatedAtAction( nameof(Get), new { userId = user.Id }, user );
            }
            catch ( UserExistsException )
            {
                return CreatedAtAction( nameof(Get), new { userId = userData.Id }, userData );
            }
            catch ( Exception )
            {
                return new StatusCodeResult( (int) HttpStatusCode.InternalServerError );
            }
        }
    }
}