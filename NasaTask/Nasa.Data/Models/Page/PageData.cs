using Newtonsoft.Json;

namespace Nasa.Data.Models.Page
{
    /// <summary>
    /// Page data for an asteroid collection from the Nasa Api
    /// </summary>
    public class PageData
    {
        /// <summary>
        /// Total number of pages.
        /// </summary>
        [JsonProperty("total_pages")]
        public int PageCount { get; set; }

        /// <summary>
        /// Current page.
        /// </summary>
        [JsonProperty("number")]
        public int PageNumber { get; set; }
    }
}
