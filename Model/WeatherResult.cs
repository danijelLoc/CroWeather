using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroWeatherUpdateService.Model
{
    public class CityWeather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Coord coord { get; set; }
        public Sys sys { get; set; }
        // public List<WeatherDescription> weather { get; set; }
        public MainWeatherInformation main { get; set; }
        public int visibility { get; set; }
        public int dt { get; set; }
        public string name { get; set; }
    }

    public class MainWeatherInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public int? sea_level { get; set; }
        public int? grnd_level { get; set; }
    }

    public class CitiesWeather
    {
        public int cnt { get; set; }
        public List<CityWeather> list { get; set; }
    }

    public class Coord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Sys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string country { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherDescription
    {
        [Key]
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
