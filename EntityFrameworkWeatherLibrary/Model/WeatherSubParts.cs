﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherDomainLibrary.Model
{
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
