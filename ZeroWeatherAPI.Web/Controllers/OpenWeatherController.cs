using Microsoft.AspNetCore.Mvc;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces.Shared;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenWeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;

        public OpenWeatherController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet("ByLatLon/")]
        public async Task<Root> Get(decimal latitude, decimal longitude)
        {
            Root result = await _openWeatherService.GetWeatherAsync(latitude, longitude);
            return result;
        }

        [HttpGet("ByCityCountry/")]
        public async Task<Root> Get(string city, string country)
        {
            Root result = await _openWeatherService.GetWeatherAsync(city, country);
            return result;
        }
    }
}
