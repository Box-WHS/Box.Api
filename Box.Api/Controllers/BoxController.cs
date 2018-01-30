using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Box.Api.Controllers.Results;
using Box.Api.Services.Boxes;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Boxes.Models;
using Box.Api.Services.Trays;
using Box.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
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

        [HttpGet("{boxId:long}/trays")]
        public async Task<IActionResult> GetTrays([FromRoute] long boxId)
        {
            try
            {
                var trays = await _boxService.GetTrays(User.GetId(), boxId);
                return Ok(trays);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("{boxId:long}/tray/{trayId:long}")]
        public async Task<IActionResult> GetTray([FromRoute] long boxId, [FromRoute] long trayId)
        {
            try
            {
                var tray = await _boxService.GetTray(User.GetId(), boxId, trayId);
                return Ok(tray);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost("{boxId:long}/tray")]
        public async Task<IActionResult> AddTray([FromRoute] long boxId, [FromBody] TrayCreationData creationData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tray = await _boxService.AddTray(User.GetId(), boxId, creationData);
                return CreatedAtAction(nameof(GetTray), new {boxId, trayId = tray.Id}, tray);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("{boxId:long}/tray/{trayId:long}/cards")]
        public async Task<IActionResult> GetCards([FromRoute] long boxId, [FromRoute] long trayId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cards = await _boxService.GetCards(User.GetId(), boxId, trayId);
                return Ok(cards);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{boxId:long}/tray/{trayId:long}/card/{cardId:long}")]
        public async Task<IActionResult> GetCard(
            [FromRoute] long boxId,
            [FromRoute] long trayId,
            [FromRoute] long cardId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var card = await _boxService.GetCard(User.GetId(), boxId, trayId, cardId);
                return Ok(card);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }   
        }

        [HttpPost("{boxId:long}/tray/{trayId:long}/card")]
        public async Task<IActionResult> AddCard(
            [FromRoute] long boxId,
            [FromRoute] long trayId,
            [FromBody] CardCreationData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var card = await _boxService.AddCard(User.GetId(), boxId, trayId, data);
                return CreatedAtAction(nameof(GetCard), new { boxId, trayId, cardId = card.Id}, card);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }   
        }
    }
}