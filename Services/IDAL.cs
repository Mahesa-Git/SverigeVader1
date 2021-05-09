using MongoDB.Driver;
using SverigeVader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigeVader.Services
{
    interface IDAL
    {
        public IMongoCollection<Measurement> MeasurementCollection();
    }
}
