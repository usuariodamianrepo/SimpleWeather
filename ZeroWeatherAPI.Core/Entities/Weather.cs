namespace ZeroWeatherAPI.Core.Entities
{
    public class Weather
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CityId { get; set; }

        public double CoordLon { get; set; }
        public double CoordLat { get; set; }

        public int WeatherId { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }

        public string Base { get; set; }

        public double MainTemp { get; set; }
        public double MainFeelsLike { get; set; }
        public double MainTempMin { get; set; }
        public double MainTempMax { get; set; }
        public int MainPressure { get; set; }
        public int MainHumidity { get; set; }
        public int MainSeaLevel { get; set; }
        public int MainGrndLevel { get; set; }

        public int Visibility { get; set; }

        public double WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public double WindGust { get; set; }

        public int CloudsAll { get; set; }

        public int Dt { get; set; }

        public int SysType { get; set; }
        public int SysId { get; set; }
        public string SysCountry { get; set; }
        public int SysSunrise { get; set; }
        public int SysSunset { get; set; }

        public int Timezone { get; set; }
        public int OpenWeatherId { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }
}
