using System.Threading.Tasks;
using WeatherForecast.Core.Interfaces;
using WeatherForecast.Core.Objects;

namespace WeatherForecast.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherServiceClient _weatherServiceClient;

        public WeatherService(IWeatherServiceClient weatherServiceClient)
        {
            _weatherServiceClient = weatherServiceClient;
        }

        public async Task<ProcessResponse<WeatherData>> GetWeatherData(string locationName)
        {
            var locationData = await _weatherServiceClient.GetLocation(locationName);
            if (!locationData.IsSuccess)
            {
                return new ProcessResponse<WeatherData>().SetState(locationData.ResponseType, locationData.Message);
            }

            if (locationData.Data.Count == 0)
            {
                return new ProcessResponse<WeatherData>().SetState(locationData.ResponseType, "No location found");
            }

            // NOTE: Picking up the first result for the purpose of this exercise.
            // The API returns an array of matches (e.g. looking for Chester returns 2 matches)
            var weatherData = await _weatherServiceClient.GetWeatherData(locationData.Data[0].WhereOnEarthId);

            return weatherData;
        }
    }
}
