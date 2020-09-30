using AutoMapper;
using WeatherForecast.Core.Objects;
using WeatherForecast.Models;

namespace WeatherForecast.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConsolidatedWeather, DayForecastViewModel>()
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.ApplicableDate))
                .ForMember(dst => dst.WeatherName, opt => opt.MapFrom(src => src.WeatherStateName))
                .ForMember(dst => dst.WeatherAbbreviation, opt => opt.MapFrom(src => src.WeatherStateAbbr));
        }
    }
}
