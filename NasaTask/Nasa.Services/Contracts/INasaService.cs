using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.PictureOfTheDay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nasa.Services.Contracts
{
    /// <summary>
    /// Nasa Service responsible for communicating with the Nasa Api.
    /// </summary>
    public interface INasaService
    {
        /// <summary>
        /// Returns a collection of Asteroid Data from the Nasa API.
        /// </summary>
        /// <param name="page">Page to get.</param>
        /// <param name="pageSize">Page Size - between 1 and 20.</param>
        /// <returns>An AsteroidCollection object containing page information as well as the asteroid collection or null if invalid page
        /// parameters.</returns>
        Task<AsteroidCollection> GetAsteroidDataCollectionAsync(int page, int pageSize);

        /// <summary>
        /// Returns the AsteroidData for a specific asteroid by id.
        /// </summary>
        /// <param name="asteroidId">Id of asteroid to get.</param>
        /// <returns>AsteroidData object or null if not found.</returns>
        Task<AsteroidData> GetAsteroidDataAsync(string asteroidId);
        
        /// <summary>
        /// Returns the astronomy picture of the day information for a specific date.
        /// </summary>
        /// <param name="date">Date to get information for.</param>
        /// <returns>The AstronomyPictureOfTheDay object or null if invalid date.</returns>
        Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync(DateTime date);
    }
}
