using System.Net.Http;
using WeatherApp;

//var weather = await WeatherAPI.Instance.GetWeather(27.18f, 31.9f);


//Console.WriteLine($"{weather.City} Weather Now: {weather.Temp:0.#}°C");

//Console.WriteLine("Enter Country");
//var country = Console.ReadLine();
//Console.WriteLine("Enter City");
//var city = Console.ReadLine();
await WeatherAPI.Instance.GetWeather("Alexandria", "Egypt");
Console.WriteLine(WeatherAPI.Instance);