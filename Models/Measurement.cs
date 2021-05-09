using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigeVader.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public Values Values { get; set; }
    }

    public class Values
    {
        public string WindDir { get; set; }
        public double Uv { get; set; }
        public double AppTemp { get; set; }
        public float Clouds { get; set; }
        public double Relativehumidity { get; set; }
        public float Temp { get; set; }
        public double WindSpeed { get; set; }

        public string Description { get; set; }
    }
}
