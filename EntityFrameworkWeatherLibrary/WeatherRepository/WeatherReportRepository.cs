using WeatherDomainLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WeatherDomainLibrary.WeatherRepository
{
    public class WeatherReportRepository
    {
        private WeatherReportContext CreateDbContext() 
        {
            // db context must be short lived, one per each db operation
            var dbContext = new WeatherReportContext();
            dbContext.Database.EnsureCreated(); // Solution without migrations for easier sharing and testing
            return dbContext;
        }

        public void SaveCitiesWeather(List<WeatherReport> weatherReports)
        {
            var dbContext = CreateDbContext();
            dbContext.WeatherReports.AddRange(weatherReports);
            dbContext.SaveChanges();
        }

        public IQueryable<WeatherReport> GetWeatherReports()
        {
            return CreateDbContext().WeatherReports
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys);
        }

        public async Task<WeatherReport> GetWeatherReport(int weatherReportId)
        {
            WeatherReport result = await CreateDbContext().WeatherReports
                .Where(x => x.WeatherReportId == weatherReportId)
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys)
                .FirstOrDefaultAsync();

            return result;
        }

        public IQueryable<WeatherReport> GetWeatherReportsForCity(string cityName) 
        {
            IQueryable<WeatherReport> query = CreateDbContext().WeatherReports.Where(x => x.Name == cityName)
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys);

            return query;
        }

        public async Task<WeatherReport> GetLatestWeatherReportForCity(string cityName)
        {
            WeatherReport result = await CreateDbContext().WeatherReports
                .Where(x => x.Name == cityName)
                .OrderByDescending(x => x.Dt)
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys)
                .FirstOrDefaultAsync();

            return result;
        }

        public object RunSqlQuery(string sqlQuery) 
        {
            var results = CreateDbContext()
                .WeatherReports
                .FromSqlRaw(sqlQuery);
            return results;
        }


    }
}
