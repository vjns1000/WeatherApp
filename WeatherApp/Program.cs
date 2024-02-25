using System.Net.Http;
using WeatherApp;

var weather = await WeatherAPI.Instance.GetWeather(27.18f, 31.9f);

Console.WriteLine($"{weather.City} Weather Now: {weather.Temp:0.#}°C");

