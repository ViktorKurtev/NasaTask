using Nasa.Data.Models.Asteroid.Abstract;
using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.Contracts;
using Nasa.Data.Models.Orbital;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nasa.Data.Models.Asteroid
{
    public class AsteroidData : BaseAsteroidData, ICloseApproachDataContainer, IOrbitalDataContainer
    {
        [JsonProperty("close_approach_data")]
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }

        [JsonProperty("orbital_data")]
        public OrbitalData OrbitalData { get; set; }
    }
}