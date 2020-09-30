using System;
using System.Text.Json.Serialization;

namespace WeatherForecast.Core.Objects
{
    public class WeatherData : WeatherLocation
    {
        [JsonPropertyName("consolidated_weather")]
        public ConsolidatedWeather[] ConsolidatedWeather { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("sun_rise")]
        public DateTime SunRise { get; set; }

        [JsonPropertyName("sun_set")]
        public DateTime SunSet { get; set; }

        [JsonPropertyName("limezone_name")]
        public string TimezoneName { get; set; }

        [JsonPropertyName("parent")]
        public WeatherLocation Parent { get; set; }

        [JsonPropertyName("sources")]
        public Source[] Sources { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }
}