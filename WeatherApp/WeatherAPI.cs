using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<string> GetWeather(float latitude, float longitude)
        {

            using HttpResponseMessage response = await _httpClient.GetAsync($"data/2.5/weather?lat={latitude}&lon={longitude}&appid=ca6fd4fd2af88711bdb089e02c43b8a4");
            return await response.Content.ReadAsStringAsync();

        }
    }
}