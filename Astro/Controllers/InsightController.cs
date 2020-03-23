using System.Collections.Generic;
using System.Linq;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers
{
    public class InsightController : Controller
    {
        private JSONParse _JSONParse;
        private NASAApi _NASAAPpi;
        private AstroDbContext _context;

        public InsightController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAAPpi = NASAApi;
            _context = context;
        }

        public IActionResult Index()
        {
            _JSONParse.GetInsightData(_NASAAPpi.GetInsightJson());

            List<Insight> insights = _context.Insights.AsNoTracking().ToList();

            return View(insights);
        }
    }
}