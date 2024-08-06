using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Web.Dtos;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public WeatherController(IWeatherService weatherService, ICityService cityService, IMapper mapper)
        {
            _weatherService = weatherService;
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherDto>>> Get()
        {
            var weathers = await _weatherService.GetAll();
            var mappedCities = _mapper.Map<IEnumerable<Weather>, IEnumerable<WeatherDto>>(weathers);

            return Ok(mappedCities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherDto>> Get(int id)
        {
            var weather = await _weatherService.GetWeatherById(id);
            var mappedWeather = _mapper.Map<Weather, WeatherDto>(weather);

            return Ok(mappedWeather);
        }

        [HttpPost]
        public async Task<ActionResult<WeatherDto>> Post([FromBody] WeatherDto Weather)
        {
            try
            {
                var createdWeather = await _weatherService.CreateWeather(_mapper.Map<WeatherDto, Weather>(Weather));

                return Ok(_mapper.Map<Weather, WeatherDto>(createdWeather));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherDto>> Put(int id, [FromBody] WeatherDto Weather)
        {
            try
            {
                var updatedWeather = await _weatherService.UpdateWeather(id, _mapper.Map<WeatherDto, Weather>(Weather));
                return Ok(_mapper.Map<Weather, WeatherDto>(updatedWeather));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            await _weatherService.DeleteWeather(id);
            return Ok($"The Weather Id:{id} was deleted.");
        }

        [HttpGet("getandsave/")]
        public async Task<ActionResult<List<WeatherDetailDto>>> GetWeatherAndSaveInfo(int id, bool showHistorical = false, int take = 10)
        {
            List<Weather> weathers = new List<Weather>();
            
            if (id == 0)
                throw new Exception("The City Id can not be 0. Select one City.");

            var weather = await _weatherService.GetWeatherAndSaveInfo(id, take);
            weathers.Add(weather);

            if (showHistorical)
            {
                weathers.Clear();
                var historical = await _weatherService.GetLastAsync(id, take);
                weathers.AddRange(historical);
            }

            var mappedWeather = _mapper.Map<IEnumerable<Weather>, IEnumerable<WeatherDetailDto>>(weathers);

            return Ok(mappedWeather);
        }
    }
}
