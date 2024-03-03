using System.Net.Http;
using WeatherApp;

//var weather = await WeatherAPI.Instance.GetWeather(27.18f, 31.9f);


//Console.WriteLine($"{weather.City} Weather Now: {weather.Temp:0.#}°C");

//Console.WriteLine("Enter Country");
//var country = Console.ReadLine();
//Console.WriteLine("Enter City");
//var city = Console.ReadLine();
List<string> cities = new List<string> { "Alexandria", "Tokyo" };
List<string> countries = new List<string> { "Egypt", "japan" };
List<WeatherProperties> weatherList = await WeatherAPI.Instance.GetWeather(cities, countries);

// Iterate over the list of weather properties and print each city's information
for (int i = 0; i < cities.Count; i++)
{
    Console.WriteLine($"Weather information for {cities[i]}, {countries[i]}:");
    Console.WriteLine($"Description: {weatherList[i].Description}");
    Console.WriteLine($"Temperature: {weatherList[i].Temp:0.#}°C");
    Console.WriteLine($"Max Temperature: {weatherList[i].TempMax:0.#}°C");
    Console.WriteLine($"Min Temperature: {weatherList[i].TempMin:0.#}°C");
    Console.WriteLine();
}

//// To display new cities' weather 
//Console.WriteLine("click 1 to search for new cities");
//int n = int.Parse(Console.ReadLine());