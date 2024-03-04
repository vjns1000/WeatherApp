using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.Metrics;

namespace WeatherApp
{
    // Singleton Design Pattern

    internal class WeatherAPI
    {
        const string _url = "https://api.openweathermap.org/";

        HttpClient _weatherHTTP;
        WeatherProperties _weatherProperties;
        GeocodeInfo[] _place;
        public readonly static WeatherAPI Instance = new WeatherAPI();


        WeatherAPI()
        {
            _weatherProperties = new WeatherProperties();
            _weatherHTTP = new HttpClient() { BaseAddress = new Uri(_url) };
        }

        private async Task<string> GetWeatherJSON(double latitude, double longitude)
        {
            using HttpResponseMessage response = await _weatherHTTP.GetAsync($"data/2.5/weather?lat={latitude}&lon={longitude}&appid=ca6fd4fd2af88711bdb089e02c43b8a4");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<WeatherProperties> GetWeather(string city, string country)
        {

            // Parse the JSON string into a JsonDocument
            _place = await GeocodeAPI._geoInstance.GetGeographyInfo(city, country);
            using (JsonDocument document = JsonDocument.Parse(await GetWeatherJSON(_place[0].latitude, _place[0].longitude)))
            {
                // Get the root element of the JSON document
                JsonElement root = document.RootElement;

                _weatherProperties.Description = root.GetProperty("weather")[0].GetProperty("description").ToString();
                var main = root.GetProperty("main");
                _weatherProperties.Temp = main.GetProperty("temp").GetDouble() - 273.15;
                _weatherProperties.TempMax = main.GetProperty("temp_max").GetDouble() - 273.15;
                _weatherProperties.TempMin = main.GetProperty("temp_min").GetDouble() - 273.15;
                _weatherProperties.Name = root.GetProperty("name").ToString();
                return _weatherProperties;

            };

        }

        public override string ToString()
        {
            return $"{_place[0].name} {_place[0].country}\n" +
                $"{_weatherProperties.TempMax:0.#}°C {_weatherProperties.TempMin:0.#}°C";
        }
    }
}