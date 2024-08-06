using FluentValidation;
using ZeroWeatherAPI.Core.Entities;

namespace ZeroWeatherAPI.Services.Validators
{
    public class WeatherValidator : AbstractValidator<Weather>
    {
        public WeatherValidator()
        {
        }
    }
}
