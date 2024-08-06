using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Core.Interfaces.Shared;
using ZeroWeatherAPI.Services.Validators;

namespace ZeroWeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly IOpenWeatherService _openWeatherService;

        public WeatherService(IUnitOfWork unitOfWork, ICityService cityService, IOpenWeatherService openWeatherService)
        {
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _openWeatherService = openWeatherService;
        }

        public async Task<Weather> CreateWeather(Weather newWeather)
        {
            WeatherValidator validator = new();
            newWeather.InsertDate = DateTime.Now;

            var validationResult = await validator.ValidateAsync(newWeather);
            if (validationResult.IsValid)
            {
                await _unitOfWork.WeatherRepository.AddAsync(newWeather);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validationResult.Errors.ToString());
            }

            return newWeather;
        }

        public async Task DeleteWeather(int weatherId)
        {
            Weather Weather = await _unitOfWork.WeatherRepository.GetByIdAsync(weatherId);
            if (Weather == null)
                throw new ArgumentException($"The Weather Id:{weatherId} not found.");

            _unitOfWork.WeatherRepository.Remove(Weather);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Weather>> GetAll()
        {
            return await _unitOfWork.WeatherRepository.GetAllAsync();
        }

        public async Task<Weather> GetWeatherById(int id)
        {
            return await _unitOfWork.WeatherRepository.GetByIdAsync(id);
        }

        public async Task<Weather> UpdateWeather(int weatherToBeUpdatedId, Weather newWeatherValues)
        {
            WeatherValidator WeatherValidator = new();

            var validationResult = await WeatherValidator.ValidateAsync(newWeatherValues);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.ToString());

            Weather weatherToBeUpdated = await _unitOfWork.WeatherRepository.GetByIdAsync(weatherToBeUpdatedId);

            if (weatherToBeUpdated == null)
                throw new ArgumentException("Invalid Weather ID while updating");

            weatherToBeUpdated.CityId = newWeatherValues.CityId;
            weatherToBeUpdated.CoordLon = newWeatherValues.CoordLon;
            weatherToBeUpdated.CoordLat = newWeatherValues.CoordLat;
            weatherToBeUpdated.WeatherId = newWeatherValues.WeatherId;
            weatherToBeUpdated.WeatherMain = newWeatherValues.WeatherMain;
            weatherToBeUpdated.WeatherDescription = newWeatherValues.WeatherDescription;
            weatherToBeUpdated.WeatherIcon = newWeatherValues.WeatherIcon;
            weatherToBeUpdated.Base = newWeatherValues.Base;
            weatherToBeUpdated.MainTemp = newWeatherValues.MainTemp;
            weatherToBeUpdated.MainFeelsLike = newWeatherValues.MainFeelsLike;
            weatherToBeUpdated.MainTempMin = newWeatherValues.MainTempMin;
            weatherToBeUpdated.MainTempMax = newWeatherValues.MainTempMax;
            weatherToBeUpdated.MainPressure = newWeatherValues.MainPressure;
            weatherToBeUpdated.MainHumidity = newWeatherValues.MainHumidity;
            weatherToBeUpdated.MainSeaLevel = newWeatherValues.MainSeaLevel;
            weatherToBeUpdated.MainGrndLevel = newWeatherValues.MainGrndLevel;
            weatherToBeUpdated.Visibility = newWeatherValues.Visibility;
            weatherToBeUpdated.WindSpeed = newWeatherValues.WindSpeed;
            weatherToBeUpdated.WindDeg = newWeatherValues.WindDeg;
            weatherToBeUpdated.WindGust = newWeatherValues.WindGust;
            weatherToBeUpdated.CloudsAll = newWeatherValues.CloudsAll;
            weatherToBeUpdated.Dt = newWeatherValues.Dt;
            weatherToBeUpdated.SysType = newWeatherValues.SysType;
            weatherToBeUpdated.SysId = newWeatherValues.SysId;
            weatherToBeUpdated.SysCountry = newWeatherValues.SysCountry;
            weatherToBeUpdated.SysSunrise = newWeatherValues.SysSunrise;
            weatherToBeUpdated.SysSunset = newWeatherValues.SysSunset;
            weatherToBeUpdated.Timezone = newWeatherValues.Timezone;
            weatherToBeUpdated.OpenWeatherId = newWeatherValues.OpenWeatherId;
            weatherToBeUpdated.Name = newWeatherValues.Name;
            weatherToBeUpdated.Cod = newWeatherValues.Cod;


            await _unitOfWork.CommitAsync();

            return await _unitOfWork.WeatherRepository.GetByIdAsync(weatherToBeUpdatedId);
        }

        public async Task<Weather> GetWeatherAndSaveInfo(int id, int take)
        {
            DateTime beforeInsertDate = DateTime.Now.AddHours(-1);

            var weathers = await GetLastAsync(id, take);
            var weather = weathers.LastOrDefault(w => w.InsertDate > beforeInsertDate);

            if (weather == null)
            {
                var city = await _cityService.GetCityById(id);
                var cityName = Uri.EscapeDataString(city.Name);
                var newWeatherValues = await _openWeatherService.GetWeatherAsync(cityName, city.Country);

                var weatherToBeInsert = new Weather();
                weatherToBeInsert.CityId = city.Id;
                weatherToBeInsert.CoordLon = newWeatherValues.Coord.Lon;
                weatherToBeInsert.CoordLat = newWeatherValues.Coord.Lat;
                weatherToBeInsert.WeatherId = newWeatherValues.Weather[0].Id;
                weatherToBeInsert.WeatherMain = newWeatherValues.Weather[0].Main;
                weatherToBeInsert.WeatherDescription = newWeatherValues.Weather[0].Description;
                weatherToBeInsert.WeatherIcon = newWeatherValues.Weather[0].Icon;
                weatherToBeInsert.Base = newWeatherValues.Base;
                weatherToBeInsert.MainTemp = newWeatherValues.Main.Temp;
                weatherToBeInsert.MainFeelsLike = newWeatherValues.Main.FeelsLike;
                weatherToBeInsert.MainTempMin = newWeatherValues.Main.TempMin;
                weatherToBeInsert.MainTempMax = newWeatherValues.Main.TempMax;
                weatherToBeInsert.MainPressure = newWeatherValues.Main.Pressure;
                weatherToBeInsert.MainHumidity = newWeatherValues.Main.Humidity;
                weatherToBeInsert.MainSeaLevel = newWeatherValues.Main.SeaLevel;
                weatherToBeInsert.MainGrndLevel = newWeatherValues.Main.GrndLevel;
                weatherToBeInsert.Visibility = newWeatherValues.Visibility;
                weatherToBeInsert.WindSpeed = newWeatherValues.Wind.Speed;
                weatherToBeInsert.WindDeg = newWeatherValues.Wind.Deg;
                weatherToBeInsert.WindGust = newWeatherValues.Wind.Gust;
                weatherToBeInsert.CloudsAll = newWeatherValues.Clouds.All;
                weatherToBeInsert.Dt = newWeatherValues.Dt;
                weatherToBeInsert.SysType = newWeatherValues.Sys.Type;
                weatherToBeInsert.SysId = newWeatherValues.Sys.Id;
                weatherToBeInsert.SysCountry = newWeatherValues.Sys.Country;
                weatherToBeInsert.SysSunrise = newWeatherValues.Sys.Sunrise;
                weatherToBeInsert.SysSunset = newWeatherValues.Sys.Sunset;
                weatherToBeInsert.Timezone = newWeatherValues.Timezone;
                weatherToBeInsert.OpenWeatherId = newWeatherValues.Id;
                weatherToBeInsert.Name = newWeatherValues.Name;
                weatherToBeInsert.Cod = newWeatherValues.Cod;

                weather = await CreateWeather(weatherToBeInsert);
            }

            return weather;
        }

        public async Task<IEnumerable<Weather>> GetLastAsync(int id, int take)
        {
            return await _unitOfWork.WeatherRepository.GetAsync(w => w.CityId == id, q => q.OrderByDescending(l => l.InsertDate), "", false, take);
        }
    }
}
