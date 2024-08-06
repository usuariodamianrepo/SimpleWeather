using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Infrastructure.Data;

namespace ZeroWeatherAPI.Infrastructure.Repositories
{
    public class WeatherRepository : BaseRepository<Weather>, IWeatherRepository
    {
        public WeatherRepository(AppDbContext context) : base(context)
        {

        }
    }
}
