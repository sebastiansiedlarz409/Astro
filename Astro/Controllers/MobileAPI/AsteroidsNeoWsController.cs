using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            return await _context.AsteroidsNeoWs.AsNoTracking().OrderByDescending(t => t.Id).ToListAsync();
        }
    }
}
