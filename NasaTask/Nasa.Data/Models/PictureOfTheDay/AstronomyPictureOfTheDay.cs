using Nasa.Data.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nasa.Data.Models.PictureOfTheDay
{
    /// <summary>
    /// Nasa Astronomy picture of the day model containing data from the api.
    /// </summary>
    public class AstronomyPictureOfTheDay
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("explanation")]
        public string Explanation { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("hdurl")]
        public string HdUrl { get; set; }

        [JsonProperty("date")]
        [Required]
        [ValidApodDate]
        public DateTime Date { get; set; }
    }
}
