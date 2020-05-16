using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers.MobileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ForumController : ControllerBase
    {
        private readonly AstroDbContext _context;

        public ForumController(AstroDbContext context)
        {
            _context = context;
        }

        // GET: api/Forum/AllTopics
        [HttpGet("AllTopics")]
        public async Task<ActionResult<IEnumerable<Topic>>> AllTopics()
        {
            return await _context.Topics.ToListAsync();
        }

        // GET: api/Forum/GetTopic/{id}
        [HttpGet("GetTopic/{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            Topic topic = await _context.Topics.Include(t=>t.Comments).FirstOrDefaultAsync(t => t.Id == id);

            return topic;
        }
    }
}