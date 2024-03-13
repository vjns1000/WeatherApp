using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.Metrics;


namespace WeatherApp;

    // Singleton Design Pattern

    internal class WeatherAPI
    {
        const string _url = "https://api.openweathermap.org/";
        HttpClient _weatherHTTP;
        public readonly static WeatherAPI Instance = new WeatherAPI();

        WeatherAPI()
        {
            _weatherHTTP = new HttpClient() { BaseAddress = new Uri(_url) };
        }

        private async Task<string> GetWeatherJSON(double latitude, double longitude)
        {
            using HttpResponseMessage response = await _weatherHTTP.GetAsync($"data/2.5/weather?lat={latitude}&lon={longitude}&appid=ca6fd4fd2af88711bdb089e02c43b8a4");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<WeatherProperties> GetWeather(double latitude, double longitude)
        {
      
            using (JsonDocument document = JsonDocument.Parse(await GetWeatherJSON(latitude, longitude)))
            {
            // Get the root element of the JSON document
            JsonElement root = document.RootElement;

                string Description = root.GetProperty("weather")[0].GetProperty("description").ToString();
                var main = root.GetProperty("main");
                double Temp = main.GetProperty("temp").GetDouble() - 273.15;
                double TempMax = main.GetProperty("temp_max").GetDouble() - 273.15;
                double TempMin = main.GetProperty("temp_min").GetDouble() - 273.15;
                string Name = root.GetProperty("name").ToString();
                string Country = root.GetProperty("sys").GetProperty("country").ToString();
                return new WeatherProperties(Temp, TempMin, TempMax, Name, Country, Description);

            };

        }

    }
