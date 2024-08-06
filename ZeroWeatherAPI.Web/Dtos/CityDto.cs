namespace ZeroWeatherAPI.Web.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Country { get; set; } = string.Empty;
    }

    public class CitySimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CitySaveDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Country { get; set; } = string.Empty;
    }
}
