using CroWeatherUpdateService.Properties;
using WeatherDomainLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CroWeatherUpdateService
{
    public class CroCityList
    {
        public static List<City> ListOfCitiesInCroatia()
        {
            MemoryStream memoryStream = new MemoryStream(Resources.CroCityList);
            List<City> cities = JsonSerializer.Deserialize<List<City>>(memoryStream, JsonSerializerCustomOptions.SnakeCaseJsonSerializerOptions).Where(city => city.Country.Equals("HR")).ToList();
            return cities;
        }

        public static List<string> IdsOfCitiesInCroatia()
        {
            List<City> cities = ListOfCitiesInCroatia();
            List<string> citiesIds = cities.ConvertAll<string>(city => city.Id.ToString());
            return citiesIds;
        }
    }
}
