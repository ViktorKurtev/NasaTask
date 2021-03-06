﻿using Newtonsoft.Json;

namespace Nasa.Data.Models.Diameter
{
    /// <summary>
    /// Estimated Diameter in Meters.
    /// </summary>
    public class Meters
    {
        [JsonProperty("estimated_diameter_min")]
        public decimal? Min { get; set; }
        [JsonProperty("estimated_diameter_max")]
        public decimal? Max { get; set; }
    }
}
