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
    public class APODController : ControllerBase
    {
        private readonly AstroDbContext _context;
        private JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;

        public APODController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        // GET: api/APOD
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APOD>>> GetAPOD()
        {
            await _JSONParse.GetTodayApodData(await _NASAApi.GetTodaysApodJson());

            return await _context.APOD.ToListAsync();
        }

        // GET: api/APOD/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APOD>> GetAPOD(int id)
        {
            var aPOD = await _context.APOD.FindAsync(id);

            if (aPOD == null)
            {
                return NotFound();
            }

            return aPOD;
        }

        // PUT: api/APOD/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPOD(int id, APOD aPOD)
        {
            if (id != aPOD.Id)
            {
                return BadRequest();
            }

            _context.Entry(aPOD).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APODExists(id))
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

        // POST: api/APOD
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<APOD>> PostAPOD(APOD aPOD)
        {
            _context.APOD.Add(aPOD);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPOD", new { id = aPOD.Id }, aPOD);
        }

        // DELETE: api/APOD/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APOD>> DeleteAPOD(int id)
        {
            var aPOD = await _context.APOD.FindAsync(id);
            if (aPOD == null)
            {
                return NotFound();
            }

            _context.APOD.Remove(aPOD);
            await _context.SaveChangesAsync();

            return aPOD;
        }

        private bool APODExists(int id)
        {
            return _context.APOD.Any(e => e.Id == id);
        }
    }
}
