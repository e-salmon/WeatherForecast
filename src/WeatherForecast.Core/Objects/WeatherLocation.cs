using System.Text.Json.Serialization;

namespace WeatherForecast.Core.Objects
{
    public class WeatherLocation
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("woeid")]
        public int WhereOnEarthId { get; set; }

        [JsonPropertyName("latt_long")]
        public string LattLong { get; set; }
    }
}
