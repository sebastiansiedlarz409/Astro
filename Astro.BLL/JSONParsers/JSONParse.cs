using Astro.BLL.Tools;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Astro.BLL.JSONParsers
{
    public class JSONParse
    {
        private readonly DateFormater _dateFormater;
        private readonly APIDbRepository _repository;
        private readonly JSONtools _jsonTools;

        public JSONParse(DateFormater dateFormater, APIDbRepository repository, JSONtools jsonTools)
        {
            _dateFormater = dateFormater;
            _repository = repository;
            _jsonTools = jsonTools;
        }

        public async Task GetTodayApodData(string data)
        {
            if (data is null)
                return;

            JObject jObject = JObject.Parse(data);

            APOD apod = new APOD()
            {
                Author = _jsonTools.GetValue<string>(jObject, "copyright"),
                Date = _jsonTools.GetValue<string>(jObject, "date"),
                Description = _jsonTools.GetValue<string>(jObject, "explanation"),
                MediaType = _jsonTools.GetValue<string>(jObject, "media_type"),
                Title = _jsonTools.GetValue<string>(jObject, "title"),
                Url = _jsonTools.GetValue<string>(jObject, "url"),
                UrlHd = _jsonTools.GetValue<string>(jObject, "hdurl")
            };

            await _repository.SavaApodInDataBase(apod);
        }

        public async Task GetEpicData(string data)
        {
            if (data is null)
                return;

            JArray json = JArray.Parse(data);

            string epicLink = "https://epic.gsfc.nasa.gov/archive/natural/";

            for (int i = 0; i < json.Count; i++)
            {
                JObject jObject = JObject.Parse(json[i].ToString());
                EPIC epic = new EPIC()
                {
                    ImageName = _jsonTools.GetValue<string>(jObject, "image"),
                    Date = _jsonTools.GetValue<string>(jObject, "date"),
                    Description = _jsonTools.GetValue<string>(jObject, "caption")
                };

                epic.ImageName = epicLink + _dateFormater.FormatEPIC(epic.Date) + "/png/" + epic.ImageName + ".png";

                await _repository.SavaEpicInDataBase(epic);
            }
        }

        public IEnumerable<Gallery> GetGalleryImages(string data)
        {
            JObject json = JObject.Parse(data);
            JArray items = _jsonTools.GetJArray(_jsonTools.GetJObject(json, "collection"), "items");

            for (int i = 0; i < items.Count; i++)
            {
                JObject temp = JObject.Parse(items[i].ToString());

                if (temp.SelectToken("links") is null || temp.SelectToken("data") is null)
                    continue;

                JObject links = JObject.Parse(_jsonTools.GetJArray(temp, "links")[0].ToString());
                JObject dataValue = JObject.Parse(_jsonTools.GetJArray(temp, "data")[0].ToString());

                Gallery gallery = new Gallery()
                {
                    Url = _jsonTools.GetValue<string>(links, "href"),
                    Description = _jsonTools.GetValue<string>(dataValue, "title")
                };

                yield return gallery;
            }
        }

        public async Task GetAsteroidsNeoWsData(string data)
        {
            if (data is null)
                return;

            JArray json = _jsonTools.GetJArray(JObject.Parse(data), "near_earth_objects");

            for (int i = 0; i < json.Count; i++)
            {
                JObject jObject = JObject.Parse(json[i].ToString());

                AsteroidsNeoWs asteroids = new AsteroidsNeoWs()
                {
                    Name = _jsonTools.GetValue<string>(jObject, "name"),
                    Url = _jsonTools.GetValue<string>(jObject, "nasa_jpl_url"),
                    Size = _jsonTools.GetValue<string>(jObject, "absolute_magnitude_h"),
                    Dangerous = _jsonTools.GetValue<string>(jObject, "is_potentially_hazardous_asteroid"),
                    FirstObservation = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "orbital_data"),
                    "first_observation_date"),
                    LastObservation = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "orbital_data"),
                    "last_observation_date")
                };

                await _repository.SavaAsteroidsNeoWsInDataBase(asteroids);
            }
        }

        public async Task GetInsightData(string data)
        {
            if (data is null)
                return;

            JObject json = JObject.Parse(data);

            JArray keys = _jsonTools.GetJArray(json, "sol_keys");

            for (int i = 0; i < keys.Count; i++)
            {
                JObject jObject = json.SelectToken(keys[i].ToString()).Value<JObject>();

                Insight insight = new Insight()
                {
                    Number = keys[i].ToString(),
                    Date = _jsonTools.GetValue<string>(jObject, "First_UTC"),
                    EndDate = _jsonTools.GetValue<string>(jObject, "Last_UTC"),
                    Season = _jsonTools.GetValue<string>(jObject, "Season"),
                    MaxTemp = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "AT"), "mx"),
                    AvgTemp = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "AT"), "av"),
                    MinTemp = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "AT"), "mn"),
                    MaxWind = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "HWS"), "mx"),
                    AvgWind = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "HWS"), "av"),
                    MinWind = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "HWS"), "mn"),
                    MaxPress = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "PRE"), "mx"),
                    AvgPress = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "PRE"), "av"),
                    MinPress = _jsonTools.GetValue<string>(_jsonTools.GetJObject(jObject, "PRE"), "mn"),
                };

                await _repository.SavaInsightBase(insight);
            }
        }
    }
}
