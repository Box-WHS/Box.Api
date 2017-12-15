using System;
using System.Threading.Tasks;
using Box.Api.Controllers.Results;
using Box.Api.Services.Trays;
using Box.Api.Services.Trays.Exceptions;
using Box.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class TrayController : Controller
    {
        private readonly ILogger<TrayController> _logger;
        private readonly ITrayService _trayService;

        public TrayController(ITrayService trayService, ILogger<TrayController> logger)
        {
            _trayService = trayService;
            _logger = logger;
        }

        [HttpGet("{trayId:long}")]
        public async Task<IActionResult> GetTray( [FromRoute]long trayId)
        {
            try
            {
                var tray = await _trayService.GetTray(User.GetId(), trayId);
                return Ok(tray);
            }
            catch (TrayHandlingException e)
            {
                _logger.LogWarning(e, $"Failed to return trays from box {trayId} for user {User.GetId()}");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new InternalServerErrorResult();
            }
        }

        [HttpPost("{boxId:long}")]
        public async Task<IActionResult> AddTrayTask([FromRoute] long boxId, [FromBody] TrayCreationData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tray = await _trayService.AddTray(User.GetId(), boxId, data);
                return CreatedAtRoute(nameof(GetTray), new {boxId = tray.BoxId, trayId = tray.Id}, tray);
            }
            catch (TrayHandlingException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }

        [HttpPatch("{trayId:long}/{newName:required:length(3,255)}")]
        public async Task<IActionResult> RenameTrayTask(
            [FromRoute] long trayId,
            [FromRoute] string name)
        {
            try
            {
                var tray = await _trayService.RenameTray(User.GetId(), trayId, name);
                return CreatedAtRoute(nameof(GetTray), new {boxId = tray.BoxId, trayId = tray.Id}, tray);
            }
            catch (TrayHandlingException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }

        [HttpDelete("{trayId:long}")]
        public async Task<IActionResult> DeleteTrayTask( [FromRoute] long trayId)
        {
            try
            {
                var tray = await _trayService.DeleteTray(User.GetId(), trayId);
                return Ok(tray);
            }
            catch (TrayHandlingException e)
            {
                _logger.LogError(e, "Couldn't find tray {0} from user {1}", trayId, User.GetId());
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Couldn't delete tray {0} from user {1}", trayId, User.GetId());
                return new InternalServerErrorResult();
            }
        }
    }
}