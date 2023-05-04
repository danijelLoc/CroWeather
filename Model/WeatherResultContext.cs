using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CroWeatherUpdateService.Model
{
    class WeatherResultContext: DbContext
    {
        public DbSet<CityWeather> WeatherResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WeatherDB;Trusted_Connection=False;user=admin;password=admin");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
