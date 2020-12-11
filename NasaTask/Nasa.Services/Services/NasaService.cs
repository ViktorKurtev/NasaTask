using Microsoft.Extensions.Configuration;
using Nasa.Data.Models.Asteroid;
using Nasa.Services.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nasa.Services.Services
{
    /// <summary>
    /// Service designed to obtain data from the Nasa API.
    /// </summary>
    public class NasaService : INasaService
    {
        private const string NasaBrowseUrl = "http://www.neowsapp.com/rest/v1/neo/browse?page={0}&size={1}&api_key={2}";
        private readonly string apiKey;
        private readonly IHttpClientFactory httpClientFactory;

        public NasaService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            apiKey = configuration.GetSection("APIKeys").GetSection("NASA").Value;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<AsteroidData>> GetAsteroidDataAsync(int page, int pageSize)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(string.Format(NasaBrowseUrl, page, pageSize, apiKey));

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AsteroidCollection>(jsonResponse).Asteroids;
        }
    }
}
