using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.PictureOfTheDay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nasa.Services.Contracts
{
    public interface INasaService
    {
        /// <summary>
        /// Returns a collection of Asteroid Data from the Nasa API.
        /// </summary>
        /// <param name="page">Page to get.</param>
        /// <param name="pageSize">Page Size - between 1 and 20.</param>
        /// <returns></returns>
        Task<IEnumerable<AsteroidData>> GetAsteroidDataCollectionAsync(int page, int pageSize);
        Task<AsteroidData> GetAsteroidDataAsync(string asteroidId);
        Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDay(DateTime date);
    }
}
