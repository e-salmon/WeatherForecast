using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecast.Core.Interfaces;
using WeatherForecast.Core.Objects;

namespace WeatherForecast.Infrastructure.MetaWeatherClient
{
    public class MetaWeatherClient : IWeatherServiceClient
    {
        private readonly HttpClient _httpClient;

        public MetaWeatherClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProcessResponse<List<WeatherLocation>>> GetLocation(string locationName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/location/search?query={locationName}");
            return await Send<List<WeatherLocation>>(request);
        }

        public async Task<ProcessResponse<WeatherData>> GetWeatherData(int whereOnEarthId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/location/{whereOnEarthId}");
            return await Send<WeatherData>(request);
        }

        private async Task<ProcessResponse<T>> Send<T>(HttpRequestMessage request) where T : class
        {
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new ProcessResponse<T>().Error(response.ReasonPhrase);
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseContent);
            return result == null
                ? new ProcessResponse<T>().Error($"Unexpected data: {responseContent}")
                : new ProcessResponse<T>(result).Success();
        }
    }
}
