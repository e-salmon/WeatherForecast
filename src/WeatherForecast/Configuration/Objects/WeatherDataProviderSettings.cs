using System;

namespace WeatherForecast.Configuration.Objects
{
    public class WeatherDataProviderSettings
    {
        public string Name { get; set; }

        public Uri ServiceUrl { get; set; }

        public string ImageUrlTemplate { get; set; }

        public Uri LogoUrl { get; set; }
    }

}
