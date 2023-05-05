using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDomainLibrary.Model
{
    public class WeatherReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeatherReportId { get; set; }

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

    public class WeatherResultsWrapper
    {
        public int Cnt { get; set; }
        public List<WeatherReport> List { get; set; }
    }
}
