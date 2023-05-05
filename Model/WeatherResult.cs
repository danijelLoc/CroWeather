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
        public int CityWeatherId { get; set; }

        [Column("CityId")]
        public int Id { get; set; } // This is city id provided by OpenWeatherMap api
        public Coord Coord { get; set; }
        public Sys Sys { get; set; }
        public List<WeatherDescription> Weather { get; set; }
        public MainWeatherInformations Main { get; set; }
        public int Visibility { get; set; }
        public int Dt { get; set; }
        public string Name { get; set; }
    }

    public class MainWeatherInformations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MainWeatherInformationId { get; set; }
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int? SeaLevel { get; set; }
        public int? GrndLevel { get; set; }
    }

    public class CitiesWeatherWrapper
    {
        public int Cnt { get; set; }
        public List<CityWeather> List { get; set; }
    }

    public class Coord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CoordId { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class Sys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SysId { get; set; }
        public string Country { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }

    public class WeatherDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeatherDescriptionId { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
