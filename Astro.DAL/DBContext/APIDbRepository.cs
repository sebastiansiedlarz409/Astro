using Astro.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Astro.DAL.DBContext
{
    public class APIDbRepository
    {
        private readonly AstroDbContext _context;

        public APIDbRepository(AstroDbContext context)
        {
            _context = context;
        }

        public async Task SavaApodInDataBase(APOD apod)
        {
            APOD check = await _context.APOD.AsNoTracking().FirstOrDefaultAsync(t => t.Date.Equals(apod.Date));

            if (check is null)
            {
                await _context.APOD.AddAsync(apod);
                await _context.SaveChangesAsync();
            }

            //remove old ones
            List<APOD> APODs = await _context.APOD.ToListAsync();

            if (APODs.Count < 10)
                return;

            foreach (APOD item in APODs)
            {
                DateTime apodDate =
                    new DateTime(Int32.Parse(item.Date.Split("-")[0]),
                    Int32.Parse(item.Date.Split("-")[1]),
                    Int32.Parse(item.Date.Split("-")[2]));

                if ((DateTime.Now - apodDate).Days > 10)
                {
                    _context.APOD.Remove(item);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task SavaEpicInDataBase(EPIC epic)
        {
            EPIC check = await _context.EPIC.AsNoTracking().FirstOrDefaultAsync(t => t.Date.Equals(epic.Date));

            if (check is null)
            {
                await _context.EPIC.AddAsync(epic);
                await _context.SaveChangesAsync();
            }

            //remove old ones
            List<EPIC> EPICs = await _context.EPIC.ToListAsync();

            if (EPICs.Count < 10)
                return;

            foreach (EPIC item in EPICs)
            {
                string date = item.Date;
                date = date.Split(" ")[0];

                DateTime apodDate =
                    new DateTime(Int32.Parse(date.Split("-")[0]),
                    Int32.Parse(date.Split("-")[1]),
                    Int32.Parse(date.Split("-")[2]));

                if ((DateTime.Now - apodDate).Days > 15)
                {
                    _context.EPIC.Remove(item);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task SavaAsteroidsNeoWsInDataBase(AsteroidsNeoWs asteroids)
        {
            AsteroidsNeoWs check = await _context.AsteroidsNeoWs.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name.Equals(asteroids.Name));

            if (check is null)
            {
                await _context.AsteroidsNeoWs.AddAsync(asteroids);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SavaInsightBase(Insight insight)
        {
            Insight check = await _context.Insights.AsNoTracking().FirstOrDefaultAsync(t => t.Date.Equals(insight.Date));

            if (check is null)
            {
                await _context.Insights.AddAsync(insight);
                await _context.SaveChangesAsync();
            }
        }
    }
}
