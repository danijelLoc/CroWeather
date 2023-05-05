﻿using WeatherDomainLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace WeatherDomainLibrary.WeatherContext
{
    public class WeatherContext: DbContext
    {
        public DbSet<CityWeather> WeatherResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // User "admin" with table creation rights is needed in server
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WeatherDB;Trusted_Connection=False;user=admin;password=admin");
        }
    }
}