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

            return await _context.APOD.AsNoTracking().OrderByDescending(t => t.Id).ToListAsync();
        }
    }
}
