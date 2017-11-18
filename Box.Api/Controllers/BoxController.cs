using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Box.Api.Controllers.Results;
using Box.Api.Services.Boxes;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Boxes.Models;
using Box.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    [Authorize]
    [Produces("application/json", "application/xml")]
    [Route("[controller]")]
    public class BoxController : Controller
    {
        private readonly IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoxes()
        {
            try
            {
                var boxes = await _boxService.GetBoxes(User.GetId());
                return Ok(boxes);
            }
            catch (BoxNotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
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
                return NotFound();
            }
            catch (Exception e)
            {
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