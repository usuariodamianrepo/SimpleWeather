using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces.Shared;

namespace ZeroWeatherAPI.Infrastructure.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        public UrlSettings _urlSettings { get; }

        public OpenWeatherService(IOptions<UrlSettings> urlSettings)
        {
            _urlSettings = urlSettings.Value;
        }

        public async Task<Root> GetWeatherAsync(decimal latitude, decimal longitude)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_urlSettings.OpenWeatherUrl}?lat={latitude}&lon={longitude}&appid={_urlSettings.OpenWeatherApiKey}";
                    
                    client.DefaultRequestHeaders.Clear();

                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    Root rootObject = JsonConvert.DeserializeObject<Root>(json);
                    return rootObject;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when it gets Request from OpenWeatherUrl. {ex.Message}");
            }
        }

        public async Task<Root> GetWeatherAsync(string city, string country)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_urlSettings.OpenWeatherUrl}?q={city},{country}&appid={_urlSettings.OpenWeatherApiKey}";

                    client.DefaultRequestHeaders.Clear();

                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    Root rootObject = JsonConvert.DeserializeObject<Root>(json);
                    return rootObject;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when it gets Request from OpenWeatherUrl. {ex.Message}");
            }
        }
    }
}