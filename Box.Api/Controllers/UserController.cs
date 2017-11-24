using System;
using System.Threading.Tasks;
using Box.Api.Services.Boxes;
using Box.Api.Services.Boxes.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json", "application/xml")]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IBoxService _boxService;

        private readonly ILogger<UserController> _logger;

        public UserController(IBoxService boxService, ILogger<UserController> logger)
        {
            _boxService = boxService;
            _logger = logger;
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
                _logger.LogInformation($"Failed to delete Boxes from user {userGuid}.");
                return new OkResult();
            }
        }
    }
}