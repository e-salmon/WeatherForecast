using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core.Objects;

namespace WeatherForecast.Core.Interfaces
{
    public interface IWeatherServiceClient
    {
        Task<ProcessResponse<List<WeatherLocation>>> GetLocation(string locationName);

        Task<ProcessResponse<WeatherData>> GetWeatherData(int whereOnEarthId);
    }
}