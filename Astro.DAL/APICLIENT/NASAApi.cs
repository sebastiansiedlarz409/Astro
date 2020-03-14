using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Astro.DAL.APICLIENT
{
    public class NASAApi
    {
        private string _apiKey = "O832RalDsOHeIhuMw3VEUvBvVOCI9LG2eF4TyzZw";

        public string GetTodaysApodJson()
        {
            string result = null;

            WebRequest request = WebRequest.Create(
              "https://api.nasa.gov/planetary/apod?api_key="+_apiKey);

            result = DownloadData(request);

            return result;
        }

        public string GetEpicJson()
        {
            string result = null;

            WebRequest request = WebRequest.Create(
              "https://api.nasa.gov/EPIC/api/natural?api_key=" + _apiKey);

            result = DownloadData(request);

            return result;
        }

        public string GetAsteroidsNeoWsJson()
        {
            string result = null;

            WebRequest request = WebRequest.Create(
              "https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=" + _apiKey);

            result = DownloadData(request);

            return result;
        }

        public string GetGalleryJson(string search)
        {
            string result = null;

            //when try to find empty string
            if (search is null)
                return result;

            WebRequest request = WebRequest.Create(
              "https://images-api.nasa.gov/search?q=" + search);

            result = DownloadData(request);

            return result;
        }

        private string DownloadData(WebRequest request)
        {
            string result = null;

            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
            }
            response.Close();

            return result;
        }
    }
}
