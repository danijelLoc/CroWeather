using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using CroWeatherUpdateService.Properties;

namespace CroWeatherUpdateService.Model
{
    public class Coordinate
    {
        public float lon { get; set; }
        public float lat { get; set; }

    }
    public class City
    {
        public long id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public Coordinate coord { get; set; }
    }
    public class CityList
    {
        public static List<City> ListOfCitiesInCroatia()
        {
            MemoryStream memoryStream = new MemoryStream(Resources.CroCityList);
            List<City> cities = JsonSerializer.Deserialize<List<City>>(memoryStream).Where(city => city.country.Equals("HR")).ToList();
            return cities;
        }

        public static List<string> IdsOfCitiesInCroatia()
        {
            List<City> cities = ListOfCitiesInCroatia();
            List<string> citiesIds = cities.ConvertAll<string>(city => city.id.ToString());
            return citiesIds;
        }
    }
}
