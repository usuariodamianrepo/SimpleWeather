using ZeroWeatherAPI.Core.Dtos;

namespace ZeroWeatherAPI.Core.Interfaces.Shared
{
    public interface IOpenWeatherService
    {
        Task<Root> GetWeatherAsync(decimal latitude, decimal longitude);
        Task<Root> GetWeatherAsync(string city, string country);
    }
}
