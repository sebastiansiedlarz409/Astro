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
    public class APODController : Controller
    {
        private readonly JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;
        private readonly AstroDbContext _context;

        public APODController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await _JSONParse.GetTodayApodData(await _NASAApi.GetTodaysApodJson());

            List<APOD> apodList = await _context.APOD.AsNoTracking().OrderByDescending(t => t.Id).ToListAsync();

            return View(apodList);
        }
    }
}