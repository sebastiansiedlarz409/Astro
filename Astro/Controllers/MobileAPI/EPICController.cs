using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.DAL.APICLIENT;
using Astro.BLL.JSONParsers;

namespace Astro.Controllers.MobileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class EPICController : ControllerBase
    {
        private readonly AstroDbContext _context;
        private JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;

        public EPICController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        // GET: api/EPIC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EPIC>>> GetEPIC()
        {
            await _JSONParse.GetEpicData(await _NASAApi.GetEpicJson());

            return await _context.EPIC.ToListAsync();
        }

        // GET: api/EPIC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EPIC>> GetEPIC(int id)
        {
            var ePIC = await _context.EPIC.FindAsync(id);

            if (ePIC == null)
            {
                return NotFound();
            }

            return ePIC;
        }

        // PUT: api/EPIC/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEPIC(int id, EPIC ePIC)
        {
            if (id != ePIC.Id)
            {
                return BadRequest();
            }

            _context.Entry(ePIC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EPICExists(id))
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

        // POST: api/EPIC
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EPIC>> PostEPIC(EPIC ePIC)
        {
            _context.EPIC.Add(ePIC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEPIC", new { id = ePIC.Id }, ePIC);
        }

        // DELETE: api/EPIC/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EPIC>> DeleteEPIC(int id)
        {
            var ePIC = await _context.EPIC.FindAsync(id);
            if (ePIC == null)
            {
                return NotFound();
            }

            _context.EPIC.Remove(ePIC);
            await _context.SaveChangesAsync();

            return ePIC;
        }

        private bool EPICExists(int id)
        {
            return _context.EPIC.Any(e => e.Id == id);
        }
    }
}
