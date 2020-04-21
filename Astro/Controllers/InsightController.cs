using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.BLL.JSONParsers;
using Astro.BLL.Tools;
using Astro.DAL.APICLIENT;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers
{
    public class InsightController : Controller
    {
        private readonly JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;
        private readonly AstroDbContext _context;
        private readonly Calculator _calculator;

        public InsightController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context, Calculator calculator)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
            _calculator = calculator;
        }

        public async Task<IActionResult> Index()
        {
            await _JSONParse.GetInsightData(await _NASAApi.GetInsightJson());

            List<Insight> insights = await _context.Insights.AsNoTracking().OrderByDescending(t=>t.Id).ToListAsync();

            _calculator.Calculate(insights);

            return View(insights);
        }
    }
}