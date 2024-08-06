using ZeroWeatherAPI.Core.Entities;

namespace ZeroWeatherAPI.Core.Interfaces.Services
{
    public interface ICityService
    {
        Task<City> GetCityById(int id);
        Task<IEnumerable<City>> GetAll();
        Task<City> CreateCity(City newCity);
        Task<City> UpdateCity(int cityToBeUpdatedId, City newCityValues);
        Task DeleteCity(int cityId);
    }
}
