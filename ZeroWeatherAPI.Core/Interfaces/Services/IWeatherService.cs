using ZeroWeatherAPI.Core.Entities;

namespace ZeroWeatherAPI.Core.Interfaces.Services
{
    public interface IWeatherService
    {
        Task<Weather> GetWeatherById(int id);
        Task<IEnumerable<Weather>> GetAll();
        Task<Weather> CreateWeather(Weather newWeather);
        Task<Weather> UpdateWeather(int weatherToBeUpdatedId, Weather newWeatherValues);
        Task DeleteWeather(int weatherId);
        Task<Weather> GetWeatherAndSaveInfo(int id, int take);
        Task<IEnumerable<Weather>> GetLastAsync(int id, int take);
    }
}
