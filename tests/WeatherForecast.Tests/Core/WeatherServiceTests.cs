using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using WeatherForecast.Core.Interfaces;
using WeatherForecast.Core.Objects;
using WeatherForecast.Core.Services;
using Xunit;

namespace WeatherForecast.Tests.Core
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task WhenErrorOnQueryingLocation_ReturnError()
        {
            var moqClient = new Mock<IWeatherServiceClient>();
            moqClient
                .Setup(m => m.GetLocation(It.IsAny<string>()))
                .ReturnsAsync(new ProcessResponse<List<WeatherLocation>>().Error("Testing Error"));

            var service = new WeatherService(moqClient.Object);
            var result = await service.GetWeatherData("Test");
            result.Should().BeEquivalentTo(new ProcessResponse<List<WeatherLocation>>().Error("Testing Error"));
        }

        [Fact]
        public async Task WhenLocationNotFound_ReturnNoLocationFoundResponse()
        {
            var moqClient = new Mock<IWeatherServiceClient>();
            moqClient
                .Setup(m => m.GetLocation(It.IsAny<string>()))
                .ReturnsAsync(new ProcessResponse<List<WeatherLocation>>(new List<WeatherLocation>()).Success());

            var service = new WeatherService(moqClient.Object);
            var result = await service.GetWeatherData("Test");
            result.Should().BeEquivalentTo(new ProcessResponse<List<WeatherLocation>>().Success("No location found"));
        }

        [Fact]
        public async Task WhenErrorOnRetrievingWeatherData_PassResponseOn()
        {
            var locationData = new List<WeatherLocation>
            {
                new WeatherLocation
                {
                    Title = "Place",
                    WhereOnEarthId = 123456
                }
            };

            var moqClient = new Mock<IWeatherServiceClient>();
            moqClient
                .Setup(m => m.GetLocation(It.IsAny<string>()))
                .ReturnsAsync(new ProcessResponse<List<WeatherLocation>>(locationData).Success());
            moqClient
                .Setup(m => m.GetWeatherData(It.IsAny<int>()))
                .ReturnsAsync(new ProcessResponse<WeatherData>().Error("Something happened"));

            var service = new WeatherService(moqClient.Object);
            var result = await service.GetWeatherData("Test");
            result.Should().BeEquivalentTo(new ProcessResponse<WeatherData>().Error("Something happened"));
        }

        [Fact]
        public async Task WhenMultipleLocation_UseFirstLocationOnly()
        {
            // NOTE: Testing for multiple as API returns an array, as there is possibility of multiple results.
            // Test also covers where only 1 location is returned, which seems the common case anyway.
            var locationData = new List<WeatherLocation>
            {
                new WeatherLocation { Title = "Place 1", WhereOnEarthId = 123456 },
                new WeatherLocation { Title = "Place 2", WhereOnEarthId = 654321 }
            };

            var moqClient = new Mock<IWeatherServiceClient>();
            moqClient
                .Setup(m => m.GetLocation(It.IsAny<string>()))
                .ReturnsAsync(new ProcessResponse<List<WeatherLocation>>(locationData).Success());
            moqClient
                .Setup(m => m.GetWeatherData(123456))
                .ReturnsAsync(
                    new ProcessResponse<WeatherData>(
                            new WeatherData
                            {
                                WhereOnEarthId = 123456,
                                Title = "Place 1",
                                LocationType = "City"
                            })
                        .Success());

            var service = new WeatherService(moqClient.Object);
            var result = await service.GetWeatherData("Test");
            moqClient.Verify(m => m.GetWeatherData(It.IsAny<int>()), Times.Once);
            moqClient.Verify(m => m.GetWeatherData(654321), Times.Never);
            result.Should().BeEquivalentTo(
                new ProcessResponse<WeatherData>(
                        new WeatherData
                        {
                            WhereOnEarthId = 123456,
                            Title = "Place 1",
                            LocationType = "City"
                        })
                    .Success());
        }
    }
}
