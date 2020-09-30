using System.Collections.Generic;

namespace WeatherForecast.Models
{
    public class ForecastViewModel
    {
        public DayForecastViewModel Today { get; set; }

        public List<DayForecastViewModel> Days { get; set; }
    }
}