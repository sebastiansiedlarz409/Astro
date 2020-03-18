using Astro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Astro.DAL.DBContext
{
    public class APIDbRepository
    {
        private AstroDbContext _context;

        public APIDbRepository(AstroDbContext context)
        {
            _context = context;
        }

        public void SavaApodInDataBase(APOD apod)
        {
            APOD check = _context.APOD.FirstOrDefault(t => t.Date.Equals(apod.Date));

            if (check is null)
            {
                _context.APOD.Add(apod);
                _context.SaveChanges();
            }

            //remove old ones
            List<APOD> APODs = _context.APOD.ToList();

            if (APODs.Count < 10)
                return;

            foreach (APOD item in APODs)
            {
                DateTime apodDate = new DateTime(Int32.Parse(item.Date.Split("-")[0]), Int32.Parse(item.Date.Split("-")[1]), Int32.Parse(item.Date.Split("-")[2]));

                if ((DateTime.Now - apodDate).Days > 10)
                {
                    _context.APOD.Remove(item);
                }
            }
            _context.SaveChanges();
        }

        public void SavaEpicInDataBase(EPIC epic)
        {
            EPIC check = _context.EPIC.FirstOrDefault(t => t.Date.Equals(epic.Date));

            if (check is null)
            {
                _context.EPIC.Add(epic);
                _context.SaveChanges();
            }

            //remove old ones
            List<EPIC> EPICs = _context.EPIC.ToList();

            if (EPICs.Count < 10)
                return;

            foreach (EPIC item in EPICs)
            {
                string date = item.Date;
                date = date.Split(" ")[0];

                DateTime apodDate = new DateTime(Int32.Parse(date.Split("-")[0]), Int32.Parse(date.Split("-")[1]), Int32.Parse(date.Split("-")[2]));

                if ((DateTime.Now - apodDate).Days > 15)
                {
                    _context.EPIC.Remove(item);
                }
            }
            _context.SaveChanges();
        }

        public void SavaAsteroidsNeoWsInDataBase(AsteroidsNeoWs asteroids)
        {
            AsteroidsNeoWs check = _context.AsteroidsNeoWs.FirstOrDefault(t => t.Name.Equals(asteroids.Name));

            if (check is null)
            {
                _context.AsteroidsNeoWs.Add(asteroids);
                _context.SaveChanges();
            }
        }

        public void SavaInsightBase(Insight insight)
        {
            Insight check = _context.Insights.FirstOrDefault(t => t.Date.Equals(insight.Date));

            if (check is null)
            {
                _context.Insights.Add(insight);
                _context.SaveChanges();
            }
        }
    }
}
