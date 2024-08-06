using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Infrastructure.Data;

namespace ZeroWeatherAPI.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {

        }
    }
}
