using FluentValidation;
using ZeroWeatherAPI.Core.Entities;

namespace ZeroWeatherAPI.Services.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(x => x.InsertDate)
                .NotNull();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(60);
            RuleFor(x => x.Description)
                .MaximumLength(250);
            RuleFor(x => x.Latitude)
                .NotNull();
            RuleFor(x => x.Longitude)
                .NotNull();
            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(2);
        }
    }
}
