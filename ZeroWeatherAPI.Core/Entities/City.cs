using System.Collections.ObjectModel;

namespace ZeroWeatherAPI.Core.Entities
{
    public class City
    {
        public City()
        {
            Weathers = new Collection<Weather>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Country { get; set; }
        public ICollection<Weather> Weathers { get; set; }
    }
}
