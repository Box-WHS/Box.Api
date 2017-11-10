using System;
using System.Threading.Tasks;
using Box.Api.Controllers.Results;
using Box.Api.Services.Boxes;
using Box.Api.Services.Boxes.Exceptions;
using Box.Core.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    //[Authorize]
    [Produces("application/json", "application/xml")]
    [Route("[controller]")]
    public class BoxController : Controller
    {
        private readonly IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [HttpGet("{userId:guid}/{boxId:long}")]
        public async Task<IActionResult> GetBox([FromRoute] Guid userId, [FromRoute] long boxId)
        {
            try
            {
                var box = await _boxService.GetBox(userId, boxId);
                return Ok(box);
            }
            catch (BoxNotFoundException e)
            {
                return NotFound();
            }
            return new InternalServerErrorResult();
        }

        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> AddNewBox([FromRoute] Guid userId, [FromBody] BoxCreationData boxCreationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var box = await _boxService.AddBox(userId, boxCreationData);

            return CreatedAtAction(nameof(GetBox), new {boxId = box.Id}, box);
        }


        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> ChangeName([FromRoute] Guid userId, [FromBody] BoxChangeName data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var box = await _boxService.ChangeName(userId, data);
            return Ok(box);
        }
    }

    public class BoxCreationData
    {
        [StringLength(3, 32)]
        public string Name { get; set; }
    }
}