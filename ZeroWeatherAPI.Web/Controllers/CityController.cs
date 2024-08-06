using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Web.Dtos;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            var cities = await _cityService.GetAll();
            var mappedCities = _mapper.Map<IEnumerable<City>, IEnumerable<CityDto>>(cities);

            return Ok(mappedCities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var city = await _cityService.GetCityById(id);
            var mappedCity = _mapper.Map<City, CityDto>(city);

            return Ok(mappedCity);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> Post([FromBody] CitySaveDto city)
        {
            try
            {
                var createdCity = await _cityService.CreateCity(_mapper.Map<CitySaveDto, City>(city));

                return Ok(_mapper.Map<City, CityDto>(createdCity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CitySaveDto city)
        {
            try
            {
                var updatedCity = await _cityService.UpdateCity(id, _mapper.Map<CitySaveDto, City>(city));
                return Ok(_mapper.Map<City, CityDto>(updatedCity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CitySimpleDto>> Delete(int id)
        {
            try
            {
                await _cityService.DeleteCity(id);
                return Ok(new CitySimpleDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
