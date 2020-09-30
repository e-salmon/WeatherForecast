using System;
using System.Text.Json.Serialization;

namespace WeatherForecast.Core.Objects
{
    public class ConsolidatedWeather
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("weather_state_name")]
        public string WeatherStateName { get; set; }

        [JsonPropertyName("weather_state_abbr")]
        public string WeatherStateAbbr { get; set; }

        [JsonPropertyName("wind_direction_compass")]
        public string WindDirectionCompass { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("applicable_date")]
        public DateTime ApplicableDate { get; set; }

        [JsonPropertyName("min_temp")]
        public float MinimumTemperature { get; set; }

        [JsonPropertyName("max_temp")]
        public float MaximumTemperature { get; set; }

        [JsonPropertyName("the_temp")]
        public float TheTemperature { get; set; }

        [JsonPropertyName("wind_speed")]
        public float WindSpeed { get; set; }

        [JsonPropertyName("wind_direction")]
        public float WindDirection { get; set; }

        [JsonPropertyName("air_pressure")]
        public float AirPressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("visibility")]
        public float Visibility { get; set; }

        [JsonPropertyName("predictability")]
        public int Predictability { get; set; }
    }
}