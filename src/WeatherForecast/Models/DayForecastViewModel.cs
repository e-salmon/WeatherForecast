using System;

namespace WeatherForecast.Models
{
    public class DayForecastViewModel
    {
        public DateTime Date { get; set; }

        public string WeatherName { get; set; }

        public string WeatherAbbreviation { get; set; }
    }
}