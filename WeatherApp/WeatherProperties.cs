using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class WeatherProperties
    {
        public double Temp { set; get; }
        public double FeelsLike { set; get; }
        public double TempMin { set; get; }
        public double TempMax { set; get; }
        public double Speed { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public string Description { set; get; }

    }
}
