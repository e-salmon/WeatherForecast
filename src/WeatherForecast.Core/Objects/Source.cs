using System.Text.Json.Serialization;

namespace WeatherForecast.Core.Objects
{
    public class Source
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("crawl_rate")]
        public int CrawlRate { get; set; }
    }
}