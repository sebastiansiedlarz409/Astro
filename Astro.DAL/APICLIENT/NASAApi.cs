using System.Net.Http;
using System.Threading.Tasks;

namespace Astro.DAL.APICLIENT
{
    public class NASAApi
    {
        private readonly string _apiKey = "O832RalDsOHeIhuMw3VEUvBvVOCI9LG2eF4TyzZw";
        private readonly HttpClient _client;

        public NASAApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetTodaysApodJson()
        {
            string request =
              "https://api.nasa.gov/planetary/apod?api_key=" + _apiKey;

            string result = await DownloadData(request);

            return result;
        }

        public async Task<string> GetEpicJson()
        {
            string request =
              "https://api.nasa.gov/EPIC/api/natural?api_key=" + _apiKey;

            string result = await DownloadData(request);

            return result;
        }

        public async Task<string> GetAsteroidsNeoWsJson()
        {
            string request =
              "https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=" + _apiKey;

            string result = await DownloadData(request);

            return result;
        }

        public async Task<string> GetInsightJson()
        {
            string request =
              "https://api.nasa.gov/insight_weather/?api_key=" + _apiKey + "&feedtype=json&ver=1.0";

            string result = await DownloadData(request);

            return result;
        }

        public ValueTask<string> GetGalleryJson(string search)
        {
            string result = null;

            //when try to find empty string
            if (search is null)
                return new ValueTask<string>(result);

            string request =
              "https://images-api.nasa.gov/search?q=" + search;

            return new ValueTask<string>(DownloadData(request));
        }

        private async Task<string> DownloadData(string request)
        {
            string result = null;
            try
            {
                result = await _client.GetStringAsync(request);
            }
            catch
            {
                return result;
            }
            return result;
        }
    }
}
