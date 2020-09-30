using System.Threading.Tasks;
using WeatherForecast.Core.Objects;

namespace WeatherForecast.Core.Interfaces
{
    public interface IWeatherService
    {
        Task<ProcessResponse<WeatherData>> GetWeatherData(string locationName);
    }
}