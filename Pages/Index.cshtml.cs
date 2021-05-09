using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SverigeVader.Models;
using SverigeVader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SverigeVader.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string City { get; set; }
        private readonly IConfiguration _configuration;
        public string ErrorMessage { get; set; }
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = new HttpClient();
            string[] cities = { "Kiruna", "Sundsvall", "Stockholm", "Jönköping", "Göteborg", "Malmö" };
            var measurements = new List<Measurement>();
            var collection = new DAL(_configuration);
            WeatherData weatherData;
            foreach (string city in cities)
            {
                try
                {
                    Task<string> getWeatherStringTask = client.GetStringAsync($"https://api.weatherbit.io/v2.0/current?key=362d47ca2d99498bb16a8b48f8d7469d&lang=sv&city=" + city);
                    string weatherString = await getWeatherStringTask;
                    weatherData = JsonSerializer.Deserialize<WeatherData>(weatherString);
                }
                catch (Exception)
                {
                    ErrorMessage = "Kunde inte hämta data från väder-APIet. Timeout.";
                    return Page();
                }
                
                
                

                var measurement = new Measurement
                {
                    Id = Guid.NewGuid(),
                    City = city,
                    Date = DateTime.Now,
                    Values = new Values
                    {
                        Relativehumidity = weatherData.data[0].rh,
                        Temp = weatherData.data[0].temp,
                        WindSpeed = Math.Round(weatherData.data[0].wind_spd),
                        Description = weatherData.data[0].weather.description,

                        Clouds = weatherData.data[0].clouds,
                        Uv = weatherData.data[0].uv,
                        WindDir = weatherData.data[0].wind_cdir_full,
                        AppTemp = weatherData.data[0].app_temp
                    }
                };

                measurements.Add(measurement);

                await collection.MeasurementCollection().InsertOneAsync(measurement);
            }
            return Page();
        }
    }
  

}
