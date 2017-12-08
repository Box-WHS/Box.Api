using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Box.Api.Controllers.Results;
using Box.Api.Services.Boxes;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Boxes.Models;
using Box.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class BoxController : Controller
    {
        private readonly IBoxService _boxService;
        private readonly ILogger _logger;

        public BoxController(IBoxService boxService, ILogger<BoxController> logger)
        {
            _boxService = boxService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoxes()
        {
            try
            {
                var boxes = await _boxService.GetBoxes(User.GetId());
                return Ok(boxes);
            }
            catch (BoxNotFoundException)
            {
                _logger.LogWarning($"Failed to get boxes for user {User.GetId()}");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new InternalServerErrorResult();
            }
        }


        [HttpGet("{boxId:long}")]
        public async Task<IActionResult> GetBox([FromRoute] long boxId)
        {
            try
            {
                var box = await _boxService.GetBox(User.GetId(), boxId);
                return Ok(box);
            }
            catch (BoxNotFoundException e)
            {
                _logger.LogWarning($"Failed to get box (id:{e.Id}) for user {User.GetId()}");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new InternalServerErrorResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBox([FromBody] BoxCreationData boxCreationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var box = await _boxService.AddBox(User.GetId(), boxCreationData);

            return CreatedAtAction(nameof(GetBox), new {boxId = box.Id}, box);
        }


        [HttpPut]
        public async Task<IActionResult> ChangeName([FromBody] BoxChangeName data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var box = await _boxService.ChangeName(User.GetId(), data);
            return Ok(box);
        }
    }
}