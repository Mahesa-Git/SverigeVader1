using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigeVader.Models
{
    public class WeatherData
    {
        public Datum[] data { get; set; }
        public int count { get; set; }
    }

    public class Datum
    {
        public double app_temp { get; set; }
        public double uv { get; set; }
        public double rh { get; set; }
        public float wind_spd { get; set; }
        public string wind_cdir_full { get; set;}
        public float clouds { get; set; }
        public Weather weather { get; set; }
        public string datetime { get; set; }
        public float temp { get; set; }
    }

    public class Weather
    {
        public string icon { get; set; }
        public int code { get; set; }
        public string description { get; set; }
    }












}
