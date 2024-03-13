using System.Net.Http;
using WeatherApp;
using System.Globalization;

void DisplayWeather(WeatherProperties weather)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"{(weather.Name + ", " + weather.Country),-20}");

    Console.ForegroundColor = ConsoleColor.Yellow; 
    Console.Write($" ({weather.Temp:0.#}°C)");

    Console.ForegroundColor = ConsoleColor.Blue; 
    Console.Write($" ({weather.TempMin:0.#}°C ");

    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write($"{weather.TempMax,-4:0.#}°C)");

    Console.ForegroundColor = ConsoleColor.Cyan;
    string Description = weather.Description;
    TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;
    string CapitalizedDescription = TextInfo.ToTitleCase(Description);
    Console.WriteLine($" {CapitalizedDescription}");

    
    Console.ResetColor();

}


var geocodeInfos = await GeocodeAPI.Instance.GetGeographyInfo("London", "England");

if (geocodeInfos.Length == 0)
{
    Console.WriteLine($"No city X in country Y");
}
else if (geocodeInfos.Length > 1)
{
    foreach(var city in geocodeInfos)
    {
        var weather = await WeatherAPI.Instance.GetWeather(city.latitude, city.longitude);
        DisplayWeather(weather);
    }
}
else
{

}