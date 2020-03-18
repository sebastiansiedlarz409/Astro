using System.Collections.Generic;
using System.Linq;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astro.Controllers
{
    public class EPICController : Controller
    {
        private JSONParse _JSONParse;
        private NASAApi _NASAAPpi;
        private AstroDbContext _context;

        public EPICController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAAPpi = NASAApi;
            _context = context;
        }

        public IActionResult Index()
        {
            _JSONParse.GetEpicData(_NASAAPpi.GetEpicJson());

            List<EPIC> epicList = _context.EPIC.ToList();

            return View(epicList);
        }
    }
}