using Nasa.Data.Contracts.Asteroid;
using Nasa.Data.Models.Asteroid.Abstract;
using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.Orbital;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nasa.Data.Models.Asteroid
{
    /// <summary>
    /// Asteroid Data for a specific asteroid.
    /// </summary>
    public class AsteroidData : BaseAsteroidData, ICloseApproachDataContainer, IOrbitalDataContainer
    {
        [JsonProperty("close_approach_data")]
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }

        [JsonProperty("orbital_data")]
        public OrbitalData OrbitalData { get; set; }
    }
}