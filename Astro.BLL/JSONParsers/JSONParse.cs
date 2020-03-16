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

        public void GetAsteroidsNeoWsData(string data)
        {
            if (data is null)
                return;

            JArray json = JObject.Parse(data).SelectToken("near_earth_objects").Value<JArray>();

            for (int i = 0; i< json.Count; i++)
            {
                JObject jObject = JObject.Parse(json[i].ToString());

                AsteroidsNeoWs asteroids = new AsteroidsNeoWs()
                {
                    Name = !(jObject.ContainsKey("name")) ? "brak" : jObject.SelectToken("name").Value<string>(),
                    Url = !(jObject.ContainsKey("nasa_jpl_url")) ? "brak" : jObject.SelectToken("nasa_jpl_url").Value<string>(),
                    Size = !(jObject.ContainsKey("absolute_magnitude_h")) ? "brak" : jObject.SelectToken("absolute_magnitude_h").Value<string>(),
                    Dangerous = !(jObject.ContainsKey("is_potentially_hazardous_asteroid")) ? "brak" : jObject.SelectToken("is_potentially_hazardous_asteroid").Value<string>(),
                    FirstObservation = !(jObject.ContainsKey("orbital_data")) ? "brak" : !(jObject.SelectToken("orbital_data").Value<JObject>().ContainsKey("first_observation_date")) ? "brak" : jObject.SelectToken("orbital_data").Value<JObject>().SelectToken("first_observation_date").Value<string>(),
                    LastObservation = !(jObject.ContainsKey("orbital_data")) ? "brak" : !(jObject.SelectToken("orbital_data").Value<JObject>().ContainsKey("last_observation_date")) ? "brak" : jObject.SelectToken("orbital_data").Value<JObject>().SelectToken("last_observation_date").Value<string>(),
                };

                SavaAsteroidsNeoWsInDataBase(asteroids);
            }
        }

        private void SavaAsteroidsNeoWsInDataBase(AsteroidsNeoWs asteroids)
        {
            AsteroidsNeoWs check = _context.AsteroidsNeoWs.FirstOrDefault(t => t.Name.Equals(asteroids.Name));

            if (check is null)
            {
                _context.AsteroidsNeoWs.Add(asteroids);
                _context.SaveChanges();
            }
        }

        public void GetInsightData(string data)
        {
            JObject json = JObject.Parse(data);

            JArray keys = json.SelectToken("sol_keys").Value<JArray>();

            for(int i = 0; i < keys.Count; i++)
            {
                JObject jObject = json.SelectToken(keys[i].ToString()).Value<JObject>();

                Insight insight = new Insight()
                {
                    Number = keys[i].ToString(),
                    Date = !(jObject.ContainsKey("First_UTC")) ? "brak" : jObject.SelectToken("First_UTC").Value<string>(),
                    Season = !(jObject.ContainsKey("Season")) ? "brak" : jObject.SelectToken("Season").Value<string>(),
                    MaxTemp = !(jObject.ContainsKey("AT")) ? "brak" : jObject.SelectToken("AT").Value<JObject>().SelectToken("mx").Value<string>(),
                    AvgTemp = !(jObject.ContainsKey("AT")) ? "brak" : jObject.SelectToken("AT").Value<JObject>().SelectToken("av").Value<string>(),
                    MinTemp = !(jObject.ContainsKey("AT")) ? "brak" : jObject.SelectToken("AT").Value<JObject>().SelectToken("mn").Value<string>(),
                    MaxWind = !(jObject.ContainsKey("HWS")) ? "brak" : jObject.SelectToken("HWS").Value<JObject>().SelectToken("mx").Value<string>(),
                    AvgWind = !(jObject.ContainsKey("HWS")) ? "brak" : jObject.SelectToken("HWS").Value<JObject>().SelectToken("av").Value<string>(),
                    MinWind = !(jObject.ContainsKey("HWS")) ? "brak" : jObject.SelectToken("HWS").Value<JObject>().SelectToken("mn").Value<string>(),
                    MaxPress = !(jObject.ContainsKey("PRE")) ? "brak" : jObject.SelectToken("PRE").Value<JObject>().SelectToken("mx").Value<string>(),
                    AvgPress = !(jObject.ContainsKey("PRE")) ? "brak" : jObject.SelectToken("PRE").Value<JObject>().SelectToken("av").Value<string>(),
                    MinPress = !(jObject.ContainsKey("PRE")) ? "brak" : jObject.SelectToken("PRE").Value<JObject>().SelectToken("mn").Value<string>()
                };

                SavaInsightBase(insight);
            }
        }

        private void SavaInsightBase(Insight insight)
        {
            Insight check = _context.Insights.FirstOrDefault(t => t.Date.Equals(insight.Date));

            if(check is null)
            {
                _context.Insights.Add(insight);
                _context.SaveChanges();
            }
        }
    }
}
