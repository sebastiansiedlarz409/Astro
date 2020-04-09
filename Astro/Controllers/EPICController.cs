using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers
{
    public class EPICController : Controller
    {
        private readonly JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;
        private readonly AstroDbContext _context;

        public EPICController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await _JSONParse.GetEpicData(await _NASAApi.GetEpicJson());

            List<EPIC> epicList = await _context.EPIC.AsNoTracking().OrderByDescending(t => t.Id).ToListAsync();

            return View(epicList);
        }
    }
}