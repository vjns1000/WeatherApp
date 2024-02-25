using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp
{
    // Singleton Design Pattern
    
    internal class WeatherAPI
    {
        const string _url = "https://api.openweathermap.org/";
        HttpClient _httpClient;

        public readonly static  WeatherAPI Instance = new WeatherAPI();


        WeatherAPI()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(_url) };
        }
        private async Task<string> GetWeatherJSON(float latitude, float longitude)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"data/2.5/weather?lat={latitude}&lon={longitude}&appid=ca6fd4fd2af88711bdb089e02c43b8a4");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<WeatherProperties> GetWeather(float latitude, float longitude)
        {
            // Parse the JSON string into a JsonDocument
            using (JsonDocument document = JsonDocument.Parse(await GetWeatherJSON(latitude, longitude)))
            {
                // Get the root element of the JSON document
                JsonElement root = document.RootElement;
                WeatherProperties weatherProperties = new WeatherProperties();
                weatherProperties.Description = root.GetProperty("weather")[0].GetProperty("description").ToString();
                var main = root.GetProperty("main");
                weatherProperties.Temp = main.GetProperty("temp").GetDouble() - 273.15;
                weatherProperties.TempMax = main.GetProperty("temp_max").GetDouble() - 273.15;
                weatherProperties.TempMin = main.GetProperty("temp_min").GetDouble() - 273.15;
                weatherProperties.City = root.GetProperty("name").ToString();
                return weatherProperties;
            };
        }
    }
}