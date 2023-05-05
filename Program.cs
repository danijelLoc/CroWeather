using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using CroWeatherUpdateService.Model;
using CroWeatherUpdateService.WeatherClient;
using CroWeatherUpdateService.WeatherContext;

namespace CroWeatherUpdateService
{
    static class Program
    {

        static async Task FetchAndSaveWeatherForAllCities()
        {
            var weatherFetcher = new CitiyWeatherFetcher();
            var weatherSaver = new CityWeatherSaver();


            List<City> croatianCitiesIds = CityList.ListOfCitiesInCroatia();

            int i = 0;
            int numberOfFetched = 0;
            do
            {
                List<CityWeather> citiesWeather = await weatherFetcher.FetchCitiesWeatherData(croatianCitiesIds.Skip(i).Take(CitiyWeatherFetcher.maxCityIdsPerCall).ToList());
                weatherSaver.SaveCitiesWeather(citiesWeather);
                numberOfFetched += citiesWeather.Count();
                i += CitiyWeatherFetcher.maxCityIdsPerCall;
            } 
            while (i < croatianCitiesIds.Count);
            Console.WriteLine(String.Format("\nGiven {0} cities, fetched and saved {1} weather reports\n", croatianCitiesIds.Count(), numberOfFetched));
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            /*
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("<<<<<<<<<<<<<<< One Service call simulation >>>>>>>>>>>>>>>>>>");
                Task.WaitAll(FetchAndSaveWeatherForAllCities());
            }
            else
            */
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new CroWeatherUpdateService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
