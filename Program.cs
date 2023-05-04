using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text.Json;
using System.Threading.Tasks;
using CroWeatherUpdateService.Model;

namespace CroWeatherUpdateService
{
    static class Program
    {
        static async Task FetchWeatherData() // TODO: Extract to dedicated functions
        {
            Console.WriteLine("One Service call simulation ---");


            var dbContext = new WeatherResultContext();
            dbContext.Database.EnsureCreated(); // Solution without migrations for easier sharing and testing
            dbContext.WeatherResults.Count();

            HttpClient client = new HttpClient();
            String weatherApiKey = "57f95bfc61602811e7ad61d65bbc773d";

            List<String> croatianCitiesIds = CityList.IdsOfCitiesInCroatia();
            String croatianCitiesIdsParameter = String.Join(",", croatianCitiesIds.Take(20));
            // var content = response.Content.ReadAsAsync<WeatherDto>().Result;

            String weatherApiUrl = String.Format("https://api.openweathermap.org/data/2.5/group?units=metric&id={0}&appid={1}", croatianCitiesIdsParameter, weatherApiKey);
            HttpResponseMessage response = (await client.GetAsync(weatherApiUrl));
            if (response.IsSuccessStatusCode)
            {
                var jstring = await response.Content.ReadAsStringAsync();
                var citiesWeather = JsonSerializer.Deserialize<CitiesWeather>(jstring);
                dbContext.WeatherResults.AddRange(citiesWeather.list);
            } else
            {
                Console.WriteLine(response.StatusCode);
            }



        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Task.WaitAll(FetchWeatherData());
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
