using System;
using System.Threading.Tasks;
using Box.Api.Services.Users;
using Box.Api.Services.Users.Exceptions;
using Box.Api.Services.Users.Models;
using Box.Core.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Box.Api.Controllers.Results.Result;

namespace Box.Api.Controllers
{
    //[Authorize]
    [Produces( "application/json", "application/xml" )]
    [Route( "[controller]" )]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService )
        {
            _userService = userService;
        }

        /// <summary>
        ///     Gets a specific <see cref="User" />, identified by the <paramref name="userId" />
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET /Users/12345678-1234-5678-abcd-qwertz123456
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>The requested user</returns>
        /// <response code="200">Returns the requested user</response>
        /// <response code="404">If there is no user with the userId</response>
        [HttpGet( "{userId:guid}" )]
        public async Task<IActionResult> Get( [FromRoute] Guid userId )
        {
            try
            {
                var user = await _userService.GetUserAsync( userId );
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
                var user = await _userService.AddUserAsync( userData );
                return CreatedAtAction( nameof(Get), new { userId = user.Id }, user );
            }
            catch ( UserExistsException )
            {
                return Conflict();
            }
            catch ( Exception )
            {
                return InternalServerError();
            }
        }

        [HttpDelete( "{userId:guid}" )]
        public async Task<IActionResult> Delete( [FromRoute] Guid userId )
        {
            try
            {
                var user = await _userService.DeleteUserAsync( userId );
                return Ok( user );
            }
            catch ( Exception )
            {
                return NotFound();
            }
        }
    }
}