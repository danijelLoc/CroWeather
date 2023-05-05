using WeatherDomainLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherDomainLibrary.WeatherRepository
{
    public class WeatherReportRepository
    {
        private readonly WeatherReportContext dbContext;

        public WeatherReportRepository()
        {
            dbContext = new WeatherReportContext();
            dbContext.Database.EnsureCreated(); // Solution without migrations for easier sharing and testing
        }

        public void SaveCitiesWeather(List<WeatherReport> weatherReports)
        {
            dbContext.WeatherReports.AddRange(weatherReports);
            dbContext.SaveChanges();
        }

        public IQueryable<WeatherReport> GetWeatherReports()
        {
            return dbContext.WeatherReports
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys);
        }

        public async Task<WeatherReport> GetWeatherReport(int weatherReportId)
        {
            WeatherReport result = await dbContext.WeatherReports
                .Where(x => x.WeatherReportId == weatherReportId)
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys)
                .FirstAsync();

            return result;
        }

        public async Task<List<WeatherReport>> GetWeatherReportsForCity(string cityName) 
        {
            IQueryable<WeatherReport> query = dbContext.WeatherReports.Where(x => x.Name == cityName)
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys);

            List<WeatherReport> result = await query.ToListAsync();
            return result;
        }

        public async Task<WeatherReport> GetLatestWeatherReportForCity(string cityName)
        {
            WeatherReport result = await dbContext.WeatherReports
                .Where(x => x.Name == cityName)
                .OrderByDescending(x => x.Dt)
                .Include(x => x.Main)
                .Include(x => x.Weather)
                .Include(x => x.Coord)
                .Include(x => x.Sys)
                .FirstAsync();

            return result;
        }
    }
}
