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
        public float Lon { get; set; }
        public float Lat { get; set; }

    }
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Coordinate Coord { get; set; }
    }
    public class CityList
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
