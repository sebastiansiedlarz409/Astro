using Astro.BLL.Tools;
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
        private DateFormater _dateFormater;

        public JSONParse(AstroDbContext context, DateFormater dateFormater)
        {
            _context = context;
            _dateFormater = dateFormater;
        }

        public void GetTodayApodData(string data)
        {
            if (data is null)
                return;

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

                if ((DateTime.Now - apodDate).Days > 10)
                {
                    _context.APOD.Remove(item);
                }
            }
            _context.SaveChanges();
        }

        public void GetEpicData(string data)
        {
            if (data is null)
                return;

            JArray json = JArray.Parse(data);

            string epicLink = "https://epic.gsfc.nasa.gov/archive/natural/";

            for (int i = 0;i < json.Count; i++)
            {
                JObject jObject = JObject.Parse(json[i].ToString());
                EPIC epic = new EPIC()
                {
                    ImageName = !(jObject.ContainsKey("image")) ? "brak" : jObject.SelectToken("image").Value<string>(),
                    Date = !(jObject.ContainsKey("date")) ? "brak" : jObject.SelectToken("date").Value<string>(),
                    Description = !(jObject.ContainsKey("caption")) ? "brak" : jObject.SelectToken("caption").Value<string>(),
                };

                epic.ImageName = epicLink + _dateFormater.FormatEPIC(epic.Date) + "/png/" + epic.ImageName + ".png";

                SavaEpicInDataBase(epic);
            }
        }

        private void SavaEpicInDataBase(EPIC epic)
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

            foreach(EPIC item in EPICs)
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

        public List<Gallery> GetGalleryImages(string data)
        {
            List<Gallery> galleryImages = new List<Gallery>();

            if (data is null)
                return galleryImages;

            JObject json = JObject.Parse(data);
            JArray items = json.SelectToken("collection").Value<JObject>().SelectToken("items").Value<JArray>();

            for (int i = 0; i < items.Count; i++)
            {

                JObject temp = JObject.Parse(items[i].ToString());

                if (temp.SelectToken("links") is null || temp.SelectToken("data") is null)
                    continue;

                JObject links = JObject.Parse(temp.SelectToken("links").Value<JArray>()[0].ToString());
                JObject dataValue = JObject.Parse(temp.SelectToken("data").Value<JArray>()[0].ToString());

                Gallery gallery = new Gallery()
                {
                    Url = links.SelectToken("href").Value<string>(),
                    Description = dataValue.SelectToken("title").Value<string>()
                };

                galleryImages.Add(gallery);
            }

            return galleryImages;
        }
    }
}
