using System;
using System.Threading.Tasks;
using Box.Api.Data.DataContexts;
using Box.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers
{
    [Produces( "application/json", "application/xml" )]
    [Route( "[controller]" )]
    public class UserController : Controller
    {
        private BoxApiDataContext _context;
        
        public UserController(BoxApiDataContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _context.FindAsync<User>(userId);
            return Ok(user);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddUser(Guid userId)
        {
            try
            {
                var user = new User()
                {
                    UserId = userId
                };
                using (_context)
                {
                    await _context.AddAsync(user);
                }
                
                return CreatedAtRoute("", new { userId = userId}, user  );
            }
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }
    }
}