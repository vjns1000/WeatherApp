using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record WeatherProperties(double Temp, double TempMin, double TempMax, string Name, string Country, string Description);

}
