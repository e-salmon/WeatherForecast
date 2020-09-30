using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Core.Interfaces;
using WeatherForecast.Core.Services;
using WeatherForecast.Infrastructure.MetaWeatherClient;
using WeatherForecast.Infrastructure.Security;

namespace WeatherForecast.Configuration.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ForecastConnection")));

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IWeatherService, WeatherService>();

            services.AddHttpClient<IWeatherServiceClient, MetaWeatherClient>(
                client => client.BaseAddress = configuration.GetSection("DataProviderSettings").GetValue<Uri>("ServiceUrl"));
        }
    }
}
