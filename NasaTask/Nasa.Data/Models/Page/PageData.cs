using Newtonsoft.Json;

namespace Nasa.Data.Models.Page
{
    public class PageData
    {
        [JsonProperty("total_pages")]
        public int PageCount { get; set; }

        [JsonProperty("number")]
        public int PageNumber { get; set; }
    }
}
