using Microsoft.Extensions.Configuration;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.PictureOfTheDay;
using Nasa.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
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
        private const string NasaLookupUrl = "https://api.nasa.gov/neo/rest/v1/neo/{0}?api_key={1}";
        private const string NasaApodUrl = "https://api.nasa.gov/planetary/apod?date={0}&api_key={1}";
        private readonly string apiKey;
        private readonly IHttpClientFactory httpClientFactory;

        public NasaService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            apiKey = configuration.GetSection("APIKeys").GetSection("NASA").Value;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<AsteroidData> GetAsteroidDataAsync(string asteroidId)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(string.Format(NasaLookupUrl, asteroidId, apiKey));

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AsteroidData>(jsonResponse);
        }

        public async Task<AsteroidCollection> GetAsteroidDataCollectionAsync(int page, int pageSize)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(string.Format(NasaBrowseUrl, page, pageSize, apiKey));

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AsteroidCollection>(jsonResponse);
        }

        public async Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDay(DateTime date)
        {
            var formattedTime = date.ToString("yyyy/M/d");

            var client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(string.Format(NasaApodUrl, formattedTime, apiKey));

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AstronomyPictureOfTheDay>(jsonResponse);
        }
    }
}
