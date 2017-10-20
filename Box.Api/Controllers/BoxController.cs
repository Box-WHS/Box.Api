using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Box.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Box.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/box")]
    public class BoxController : Controller
    {
        private DataContext DataContext { get; set; }

        public BoxController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var boxes = await DataContext.Boxes.ToListAsync();
            return Ok(boxes);
        }
    }
}