using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDomainLibrary.Model;
using WeatherDomainLibrary.WeatherContext;
using CroWeatherUpdateService.WeatherClient;

namespace CroWeatherUpdateService
{
    class WeatherUpdater
    {
        private readonly List<City> listOfCities;
        private readonly CityWeatherFetcher weatherFetcher = new CityWeatherFetcher();
        private readonly CityWeatherSaver weatherSaver = new CityWeatherSaver();

        public WeatherUpdater(List<City> listOfCities)
        {
            this.listOfCities = listOfCities;
        }

        public async Task FetchAndSaveWeatherForAllCities()
        {
            int i = 0;
            int numberOfFetched = 0;
            do
            {
                List<CityWeather> citiesWeather = await weatherFetcher.FetchCitiesWeatherData(listOfCities.Skip(i).Take(CityWeatherFetcher.maxCityIdsPerCall).ToList());
                weatherSaver.SaveCitiesWeather(citiesWeather);
                numberOfFetched += citiesWeather.Count();
                i += CityWeatherFetcher.maxCityIdsPerCall;
            }
            while (i < listOfCities.Count);
            Console.WriteLine(String.Format("\nGiven {0} cities, fetched and saved {1} weather reports\n", listOfCities.Count(), numberOfFetched));
        }
    }
}
