using System.Net.Http;
using WeatherApp;
using System.Globalization;
using System.Diagnostics.Metrics;

// For the default screen. Display Asyut weather
List<GeocodeInfo> CountryCityList = new List<GeocodeInfo>();
CountryCityList.Add((await GeocodeAPI.Instance.GetGeographyInfo("Asyut", "Egypt"))[0]); // TDOO: Get actual location?
DisplayWeather(await WeatherAPI.Instance.GetWeather(CountryCityList[0].latitude, CountryCityList[0].latitude));
while (true)
{
    // For entering new cities
    Console.WriteLine("Press a key to enter new cities.");
    Console.ReadKey();
    Console.Clear();

    Console.Write("Enter City: ");
    string? city = Console.ReadLine();

    Console.Write("Enter Country: ");
    string? country = Console.ReadLine();


    var geocodeInfo = await GeocodeAPI.Instance.GetGeographyInfo(city, country);
    await CheckGeocodeEnteries(geocodeInfo);

    Thread.Sleep(3000);
    Console.Clear();

    foreach ( var cityInfo in CountryCityList)
    {
        var weather = WeatherAPI.Instance.GetWeather(cityInfo.latitude, cityInfo.longitude);
        DisplayWeather(await weather);
      
    }

}
async Task CheckGeocodeEnteries(GeocodeInfo[] geocodeInfos)
{
    if (geocodeInfos.Length == 0)
    {
        Console.WriteLine($"No such city in that country");
    }
    else if(geocodeInfos.Length == 1)
    {
        CountryCityList.Add(geocodeInfos[0]);
    }
    else
    {
        HandleSearchEntries(geocodeInfos);
    }
}

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
void HandleSearchEntries(GeocodeInfo[] Entries)
{
    int selectedCityInfo = 0;
    Console.WriteLine("Click the number of targeted city and country");
    for (int i = 0; i < Entries.Length; i++)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(i+1 + ") ");

        Console.ResetColor();
        Console.WriteLine($"{Entries[i].name}, {Entries[i].country}");
    }

    selectedCityInfo = int.Parse(Console.ReadLine());
    CountryCityList.Add(Entries[selectedCityInfo - 1]);
}