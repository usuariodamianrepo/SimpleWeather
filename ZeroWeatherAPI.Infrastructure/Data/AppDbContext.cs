using Microsoft.EntityFrameworkCore;
using ZeroWeatherAPI.Core.Entities;

namespace ZeroWeatherAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Weather> Weathers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
