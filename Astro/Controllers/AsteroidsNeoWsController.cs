using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astro.Controllers
{
    public class AsteroidsNeoWsController : Controller
    {
        private JSONParse _JSONParse;
        private NASAApi _NASAAPpi;
        private AstroDbContext _context;

        public AsteroidsNeoWsController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAAPpi = NASAApi;
            _context = context;
        }

        public IActionResult Index()
        {
            _JSONParse.GetAsteroidsNeoWsData(_NASAAPpi.GetAsteroidsNeoWsJson());

            List<AsteroidsNeoWs> asteroidsList = _context.AsteroidsNeoWs.ToList();

            return View(asteroidsList);
        }
    }
}