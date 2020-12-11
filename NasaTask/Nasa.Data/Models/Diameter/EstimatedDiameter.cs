using Newtonsoft.Json;

namespace Nasa.Data.Models.Diameter
{
    public class EstimatedDiameter
    {
        [JsonProperty("kilometers")]
        public Kilometers Kilometers { get; set; }

        [JsonProperty("meters")]
        public Meters Meters { get; set; }

        [JsonProperty("miles")]
        public Miles miles { get; set; }

        [JsonProperty("feet")]
        public Feet feet { get; set; }
    }
}
