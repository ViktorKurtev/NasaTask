using System.Collections.Generic;

namespace Nasa.Data.Models.ViewModels
{
    /// <summary>
    /// Asteroid Page View model containing a collection of asteroids as well as page information from the Nasa Api.
    /// </summary>
    public class AsteroidPageViewModel
    {
        /// <summary>
        /// Current page.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Total pages.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Asteroid View Model collection.
        /// </summary>
        public IEnumerable<AsteroidViewModel> Asteroids { get; set; }
    }
}
