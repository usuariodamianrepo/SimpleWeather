using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Services.Validators;

namespace ZeroWeatherAPI.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<City> CreateCity(City newCity)
        {
            CityValidator validator = new();
            newCity.InsertDate = DateTime.Now;

            var validationResult = await validator.ValidateAsync(newCity);
            if (validationResult.IsValid)
            {
                await _unitOfWork.CityRepository.AddAsync(newCity);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validationResult.Errors.ToString());
            }

            return newCity;
        }

        public async Task DeleteCity(int cityId)
        {
            City city = await _unitOfWork.CityRepository.GetByIdAsync(cityId);
            if(city == null)
                throw new ArgumentException($"The City Id:{cityId} not found.");

            _unitOfWork.CityRepository.Remove(city);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _unitOfWork.CityRepository.GetAllAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            return await _unitOfWork.CityRepository.GetByIdAsync(id);
        }

        public async Task<City> UpdateCity(int cityToBeUpdatedId, City newCityValues)
        {
            CityValidator cityValidator = new();

            var validationResult = await cityValidator.ValidateAsync(newCityValues);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.ToString());

            City cityToBeUpdated = await _unitOfWork.CityRepository.GetByIdAsync(cityToBeUpdatedId);

            if (cityToBeUpdated == null)
                throw new ArgumentException("Invalid City ID while updating");

            cityToBeUpdated.UpdateDate = DateTime.Now;
            cityToBeUpdated.Name = newCityValues.Name;
            cityToBeUpdated.Description = newCityValues.Description;
            cityToBeUpdated.Latitude = newCityValues.Latitude;
            cityToBeUpdated.Longitude = newCityValues.Longitude;
            cityToBeUpdated.Country = newCityValues.Country;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.CityRepository.GetByIdAsync(cityToBeUpdatedId);
        }
    }
}
