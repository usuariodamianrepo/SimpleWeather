namespace ZeroWeatherAPI.Web.Dtos
{
    public class WeatherDetailDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public double Weather { get; set; }
        public double ThermalSensation { get; set; }
    }
}
