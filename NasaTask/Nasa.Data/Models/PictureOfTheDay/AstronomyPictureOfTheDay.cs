using Nasa.Data.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nasa.Data.Models.PictureOfTheDay
{
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
        [DateTimeRange]
        public DateTime Date { get; set; }
    }
}
