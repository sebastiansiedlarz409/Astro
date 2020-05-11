using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            return await _context.EPIC.AsNoTracking().OrderByDescending(t => t.Id).ToListAsync();
        }
    }
}
