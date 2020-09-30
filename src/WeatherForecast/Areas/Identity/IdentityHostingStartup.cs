using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WeatherForecast.Areas.Identity.IdentityHostingStartup))]
namespace WeatherForecast.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}