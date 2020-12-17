using Nasa.Data.Models.Page;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nasa.Data.Models.Asteroid
{
    public class AsteroidCollection
    {
        [JsonProperty("page")]
        public PageData PageData { get; set; }

        [JsonProperty("near_earth_objects")]
        public IEnumerable<AsteroidData> Asteroids { get; set; }
    }
}
