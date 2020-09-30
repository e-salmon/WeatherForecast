using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Configuration;
using WeatherForecast.Configuration.Extensions;

namespace WeatherForecast
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InternalConfigureServices(services);
            services.AddRazorPages();
        }

        [UsedImplicitly]
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            InternalConfigureServices(services);
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");
            // NOTE: Default HSTS value is 30 days. See https://aka.ms/aspnetcore-hsts for more info.
            app.UseHsts();
            app.UseHttpsRedirection();

            InternalConfigureApp(app);
        }

        [UsedImplicitly]
        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            InternalConfigureApp(app);
        }

        private void InternalConfigureServices(IServiceCollection services)
        {
            services.ConfigureDependencyInjection(_configuration);
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllersWithViews().AddViewComponentsAsServices();
        }

        private static void InternalConfigureApp(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Ensure all routes require login
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
                endpoints.MapRazorPages();
            });
        }
    }
}
