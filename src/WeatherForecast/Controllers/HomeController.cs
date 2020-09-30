using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Core.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public HomeController(IWeatherService weatherService, IMapper mapper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
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
                Today = _mapper.Map<DayForecastViewModel>(data.Data.ConsolidatedWeather.First()),
                Days = _mapper.Map<List<DayForecastViewModel>>(data.Data.ConsolidatedWeather.Skip(1))
            };

            return PartialView("Partials/_Forecast", result);
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
