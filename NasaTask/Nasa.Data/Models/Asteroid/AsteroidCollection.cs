using Nasa.Data.Models.Page;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nasa.Data.Models.Asteroid
{
    /// <summary>
    /// Asteroid Collection object that contains page information about the asteroids from the nasa Api as well as a collection
    /// of the actual asteroid data.
    /// </summary>
    public class AsteroidCollection
    {
        [JsonProperty("page")]
        public PageData PageData { get; set; }

        [JsonProperty("near_earth_objects")]
        public IEnumerable<AsteroidData> Asteroids { get; set; }
    }
}
