using Astro.DAL.Models;
using Microsoft.EntityFrameworkCore;
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

            if (APODs.Count <= 7)
                return;

            for (int i = 0; i < APODs.Count - 7;)
            {
                if (APODs[i].MediaType.Equals("image"))
                {
                    _context.APOD.Remove(APODs[i]);
                    i++;
                }
                else
                {
                    continue;
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

            if (EPICs.Count <= 20)
                return;

            for (int i = 0; i < EPICs.Count - 20; i++)
            {
                _context.EPIC.Remove(EPICs[i]);
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

            //remove old ones
            List<AsteroidsNeoWs> asteroidsList = await _context.AsteroidsNeoWs.ToListAsync();

            if (asteroidsList.Count <= 50)
                return;

            for (int i = 0; i < asteroidsList.Count - 20; i++)
            {
                _context.AsteroidsNeoWs.Remove(asteroidsList[i]);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SavaInsightBase(Insight insight)
        {
            Insight check = await _context.Insights.AsNoTracking().FirstOrDefaultAsync(t => t.Date.Equals(insight.Date));

            if (check is null)
            {
                await _context.Insights.AddAsync(insight);
                await _context.SaveChangesAsync();
            }

            //remove old ones
            List<Insight> insightList = await _context.Insights.ToListAsync();

            if (insightList.Count <= 50)
                return;

            for (int i = 0; i < insightList.Count - 20; i++)
            {
                _context.Insights.Remove(insightList[i]);
            }

            await _context.SaveChangesAsync();
        }
    }
}
