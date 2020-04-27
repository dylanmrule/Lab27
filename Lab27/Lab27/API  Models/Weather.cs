using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab27.API__Models
{
    public class Weather
    {
        public WeatherData Data { get; set; }
        public WeatherTime Time { get; set; }
        public string productionCenter { get; set; }
    }
}
