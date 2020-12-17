using Newtonsoft.Json;

namespace Nasa.Data.Models.Orbital
{
    /// <summary>
    /// Class containing the Orbit Class data from the Nasa Api for one specific asteroid.
    /// </summary>
    public class OrbitClass
    {
        [JsonProperty("orbit_class_type")]
        public string OrbitClassType { get; set; }

        [JsonProperty("orbit_class_description")]
        public string OrbitClassDescription { get; set; }

        [JsonProperty("orbit_class_range")]
        public string OrbitClassRange { get; set; }
    }
}
