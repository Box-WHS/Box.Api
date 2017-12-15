using System;
using System.Threading.Tasks;
using Box.Api.Controllers.Results;
using Box.Api.Services.Boxes;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Exceptions;
using Box.Core.DataTransferObjects;
using Box.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;

namespace Box.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly BoxService _boxService;

        private readonly ILogger<UserController> _logger;

        public UserController(IBoxService boxService, ILogger<UserController> logger)
        {
            _boxService = boxService as BoxService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser()
        {
            try
            {
                var user = await _boxService.AddUser(User.GetId());
                return CreatedAtAction(nameof(GetUser), new {userId = user.Guid}, user.UserDto());
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> AddUser( [FromRoute]Guid userId)
        {
            try
            {
                var user = await _boxService.AddUser(userId);
                return CreatedAtAction(nameof(GetUser), new {userId = user.Guid}, user.UserDto());
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            try
            {
                var user = await _boxService.GetUser(userId.ToGuid());
                return new OkObjectResult(user.UserDto());
            }
            catch (UserNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] Guid userGuid)
        {
            try
            {
                var boxes = await _boxService.DeleteBoxes(userGuid);
                return new OkObjectResult(boxes);
            }
            catch (BoxNotFoundException e)
            {
                _logger.LogInformation(e, $"Failed to delete Boxes from user {userGuid}.");
                return new OkResult();
            }
        }
    }
}