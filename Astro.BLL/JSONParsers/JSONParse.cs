using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Astro.BLL.JSONParsers
{
    public class JSONParse
    {
        private AstroDbContext _context;

        public JSONParse(AstroDbContext context)
        {
            _context = context;
        }

        public void GetTodayApodData(string data)
        {
            JObject json = JObject.Parse(data);

            APOD apod = new APOD()
            {
                Author = !(json.ContainsKey("copyright")) ? "brak" : json.SelectToken("copyright").Value<string>(),
                Date = !(json.ContainsKey("date")) ? "brak" : json.SelectToken("date").Value<string>(),
                Description = !(json.ContainsKey("explanation")) ? "brak" : json.SelectToken("explanation").Value<string>(),
                MediaType = !(json.ContainsKey("media_type")) ? "brak" : json.SelectToken("media_type").Value<string>(),
                Title = !(json.ContainsKey("title")) ? "brak" : json.SelectToken("title").Value<string>(),
                Url = !(json.ContainsKey("url")) ? "brak" : json.SelectToken("url").Value<string>(),
                UrlHd = !(json.ContainsKey("hdurl")) ? !(json.ContainsKey("url")) ? "brak" : json.SelectToken("url").Value<string>() : json.SelectToken("hdurl").Value<string>()
            };

            SavaApodInDataBase(apod);
        }

        private void SavaApodInDataBase(APOD apod)
        {
            APOD check = _context.APOD.FirstOrDefault(t => t.Date.Equals(apod.Date));

            if(check is null)
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
                var a = (DateTime.Now - apodDate).Days;
                if ((DateTime.Now - apodDate).Days > 10)
                {
                    _context.APOD.Remove(item);
                }
            }
            _context.SaveChanges();
        }
    }
}
