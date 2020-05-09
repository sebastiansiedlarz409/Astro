using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;

namespace Astro.Controllers.MobileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsteroidsNeoWsController : ControllerBase
    {
        private readonly AstroDbContext _context;
        private JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;

        public AsteroidsNeoWsController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        // GET: api/AsteroidsNeoWs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsteroidsNeoWs>>> GetAsteroidsNeoWs()
        {
            await _JSONParse.GetAsteroidsNeoWsData(await _NASAApi.GetAsteroidsNeoWsJson());

            return await _context.AsteroidsNeoWs.ToListAsync();
        }

        // GET: api/AsteroidsNeoWs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AsteroidsNeoWs>> GetAsteroidsNeoWs(int id)
        {
            var asteroidsNeoWs = await _context.AsteroidsNeoWs.FindAsync(id);

            if (asteroidsNeoWs == null)
            {
                return NotFound();
            }

            return asteroidsNeoWs;
        }

        // PUT: api/AsteroidsNeoWs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsteroidsNeoWs(int id, AsteroidsNeoWs asteroidsNeoWs)
        {
            if (id != asteroidsNeoWs.Id)
            {
                return BadRequest();
            }

            _context.Entry(asteroidsNeoWs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsteroidsNeoWsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AsteroidsNeoWs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AsteroidsNeoWs>> PostAsteroidsNeoWs(AsteroidsNeoWs asteroidsNeoWs)
        {
            _context.AsteroidsNeoWs.Add(asteroidsNeoWs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsteroidsNeoWs", new { id = asteroidsNeoWs.Id }, asteroidsNeoWs);
        }

        // DELETE: api/AsteroidsNeoWs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AsteroidsNeoWs>> DeleteAsteroidsNeoWs(int id)
        {
            var asteroidsNeoWs = await _context.AsteroidsNeoWs.FindAsync(id);
            if (asteroidsNeoWs == null)
            {
                return NotFound();
            }

            _context.AsteroidsNeoWs.Remove(asteroidsNeoWs);
            await _context.SaveChangesAsync();

            return asteroidsNeoWs;
        }

        private bool AsteroidsNeoWsExists(int id)
        {
            return _context.AsteroidsNeoWs.Any(e => e.Id == id);
        }
    }
}
