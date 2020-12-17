using Newtonsoft.Json;

namespace Nasa.Data.Models.CloseApproach
{
    /// <summary>
    /// Relatively Velocity object that is used in a Close Approach Data object.
    /// </summary>
    public class RelativeVelocity
    {
        [JsonProperty("kilometers_per_second")]
        public string KilometersPerSecond { get; set; }

        [JsonProperty("kilometers_per_hour")]
        public string KilometersPerHour { get; set; }

        [JsonProperty("miles_per_hour")]
        public string MilesPerHour { get; set; }
    }
}
