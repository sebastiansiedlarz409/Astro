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
            string result = "";

            WebRequest request = WebRequest.Create(
              "https://api.nasa.gov/planetary/apod?api_key="+_apiKey);

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
