using Newtonsoft.Json;

namespace Nasa.Data.Models.CloseApproach
{
    /// <summary>
    /// Miss distance object used in a Close Approach Data object.
    /// </summary>
    public class MissDistance
    {
        [JsonProperty("astronomical")]
        public string Astronomical { get; set; }

        [JsonProperty("lunar")]
        public string Lunar { get; set; }

        [JsonProperty("kilometers")]
        public string Kilometers { get; set; }

        [JsonProperty("miles")]
        public string Miles { get; set; }
    }
}
