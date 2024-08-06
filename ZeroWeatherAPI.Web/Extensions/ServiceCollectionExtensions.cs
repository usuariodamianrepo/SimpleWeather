using Microsoft.EntityFrameworkCore;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Core.Interfaces.Shared;
using ZeroWeatherAPI.Infrastructure.Data;
using ZeroWeatherAPI.Infrastructure.Repositories;
using ZeroWeatherAPI.Infrastructure.Services;
using ZeroWeatherAPI.Services;

namespace ZeroWeatherAPI.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        }

        public static void AddRepositoryCore(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
            services.AddScoped(typeof(IWeatherRepository), typeof(WeatherRepository));

            services.AddScoped(typeof(ICityService), typeof(CityService));
            services.AddScoped(typeof(IWeatherService), typeof(WeatherService));
        }

        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UrlSettings>(configuration.GetSection("UrlSettings"));
            services.AddTransient<IOpenWeatherService,OpenWeatherService>();
        }
    }
}
