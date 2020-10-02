using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherForecast.Configuration.Objects;
using WeatherForecast.Core.Interfaces;
using WeatherForecast.Core.Objects;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;
        private readonly WeatherDataProviderSettings _dataProviderSettings;

        public HomeController(IWeatherService weatherService, IMapper mapper, IOptions<WeatherDataProviderSettings> dataProviderSettings)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _dataProviderSettings = dataProviderSettings.Value;
        }

        public IActionResult Index()
        {
            return View(_dataProviderSettings);
        }

        [HttpGet("forecast-update")]
        public async Task<IActionResult> UpdateForecast()
        {
            // NOTE: Make dynamic by accepting a location name from the front end.
            var data = await _weatherService.GetWeatherData("Belfast");
            if (!data.IsSuccess)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            var result = new ForecastViewModel
            {
                Today = _mapper.Map<ConsolidatedWeather, DayForecastViewModel>(
                    data.Data.ConsolidatedWeather.First(),
                    opt => opt.AfterMap((src, dst) => dst.WeatherImage = string.Format(_dataProviderSettings.ImageUrlTemplate, dst.WeatherImage))),
                Days = _mapper.Map<IEnumerable<ConsolidatedWeather>, List<DayForecastViewModel>>(
                    data.Data.ConsolidatedWeather.Skip(1),
                    opt => opt.AfterMap((src, dst) => dst.ForEach(d => d.WeatherImage = string.Format(_dataProviderSettings.ImageUrlTemplate, d.WeatherImage))))
            };

            return Ok(result);
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
