using System.Collections.Generic;
using CroWeatherUpdateService.Model;

namespace CroWeatherUpdateService.WeatherContext
{
    class CityWeatherSaver
    {
        private readonly WeatherContext dbContext;

        public CityWeatherSaver()
        {
            this.dbContext = new WeatherContext();
            this.dbContext.Database.EnsureCreated(); // Solution without migrations for easier sharing and testing
        }

        public void SaveCitiesWeather(List<CityWeather> citiesWeather)
        {
            dbContext.WeatherResults.AddRange(citiesWeather);
            dbContext.SaveChanges();
        }
    }
}
