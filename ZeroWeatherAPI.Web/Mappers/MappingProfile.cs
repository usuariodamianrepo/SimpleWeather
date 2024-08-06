using AutoMapper;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Web.Dtos;

namespace ZeroWeatherAPI.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Entities to Models
            CreateMap<City, CitySimpleDto>().PreserveReferences();
            CreateMap<City, CityDto>().PreserveReferences();
            CreateMap<Weather, WeatherDto>().PreserveReferences();
            CreateMap<Weather, WeatherDetailDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.CityName, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.SysCountry))
                .ForMember(d => d.Weather, opt => opt.MapFrom(src => src.MainTemp))
                .ForMember(d => d.ThermalSensation, opt => opt.MapFrom(src => src.MainFeelsLike));

            //Models to Entities
            CreateMap<CitySaveDto, City>();
        }
    }
}
