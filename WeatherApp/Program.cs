using System.Net.Http;
using WeatherApp;

Console.WriteLine(await WeatherAPI.Instance.GetWeather(27.18f, 31.9f));
