using System.Net.Http;
using WeatherApp;

// For the default screen. Display Asyut weather.
await WeatherAPI.Instance.GetWeather("Asyut", "Egypt");
Console.WriteLine($"{WeatherAPI.Instance}\nPress any key to enter new cities.");
Console.ReadKey();
Console.Clear(); // Clears the console screen

Console.WriteLine("Enter cities and their countries. Enter 'exit' to stop.");

var CityCountryList = new List<Tuple<string, string>>();
while (true)
{
    Console.Write("Enter City: ");
    var city = Console.ReadLine();

    if (city == "exit")
        break;
    Console.Write("Enter Country: ");
    var country = Console.ReadLine();


    CityCountryList.Add(new(city, country));

}
// Display Cities and countries in a table.
Console.WriteLine("\nCities and their corresponding countries:");
Console.WriteLine("-------------------------------------------------------------------");
Console.WriteLine("City:                                   | Country:");
Console.WriteLine("----------------------------------------|--------------------------");

foreach (var pair in CityCountryList)
{
    Console.WriteLine($"{pair.Item1,-40} | {pair.Item2,-30}");
}

Console.WriteLine("-------------------------------------------------------------------");
Console.Clear();

// Print City name, Min and Max temperature.
for (int i = 0; i < CityCountryList.Count; i++)
{
    await WeatherAPI.Instance.GetWeather(CityCountryList[i].Item1, CityCountryList[i].Item2);
    Console.WriteLine(WeatherAPI.Instance);

}