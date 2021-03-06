﻿using System.Collections.Generic;
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
    public class AsteroidsNeoWsController : Controller
    {
        private readonly JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;
        private readonly AstroDbContext _context;

        public AsteroidsNeoWsController(JSONParse JSONParse, NASAApi NASAApi, AstroDbContext context)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await _JSONParse.GetAsteroidsNeoWsData(await _NASAApi.GetAsteroidsNeoWsJson());

            List<AsteroidsNeoWs> asteroidsList = await _context.AsteroidsNeoWs.AsNoTracking().ToListAsync();

            return View(asteroidsList);
        }
    }
}