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
    public class InsightController : ControllerBase
    {
        private readonly AstroDbContext _context;
        private JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;

        public InsightController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        // GET: api/Insight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insight>>> GetInsights()
        {
            await _JSONParse.GetInsightData(await _NASAApi.GetInsightJson());

            return await _context.Insights.ToListAsync();
        }

        // GET: api/Insight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insight>> GetInsight(int id)
        {
            var insight = await _context.Insights.FindAsync(id);

            if (insight == null)
            {
                return NotFound();
            }

            return insight;
        }

        // PUT: api/Insight/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsight(int id, Insight insight)
        {
            if (id != insight.Id)
            {
                return BadRequest();
            }

            _context.Entry(insight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsightExists(id))
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

        // POST: api/Insight
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Insight>> PostInsight(Insight insight)
        {
            _context.Insights.Add(insight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsight", new { id = insight.Id }, insight);
        }

        // DELETE: api/Insight/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Insight>> DeleteInsight(int id)
        {
            var insight = await _context.Insights.FindAsync(id);
            if (insight == null)
            {
                return NotFound();
            }

            _context.Insights.Remove(insight);
            await _context.SaveChangesAsync();

            return insight;
        }

        private bool InsightExists(int id)
        {
            return _context.Insights.Any(e => e.Id == id);
        }
    }
}
