using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using WeatherDomainLibrary.Model;
using WeatherDomainLibrary.WeatherRepository;

namespace CroWeatherApi.Repository
{
    public class CachedWeatherRepository
    {
        private static CachedWeatherRepository instance = null;
        private readonly WeatherReportRepository repository = new WeatherReportRepository();
        private readonly CacheItemPolicy cacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
        private MemoryCache weatherCache = new MemoryCache("WeatherCache");

        private CachedWeatherRepository() {}

        public static CachedWeatherRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CachedWeatherRepository();
                }
                return instance;
            }
        }

        public async Task<WeatherReport> GetWeatherReportForId(int weatherReportId)
        {
            object cachedReport = weatherCache.Get(weatherReportId.ToString());
            if (cachedReport == null)
            {
                WeatherReport weatherReport = await repository.GetWeatherReport(weatherReportId);
                if (weatherReport != null)
                    weatherCache.Add(weatherReportId.ToString(), weatherReport, cacheItemPolicy);
                return weatherReport;
            }
            else
            {
                return cachedReport as WeatherReport;
            }
        }

        public async Task<WeatherReport> GetLatestWeatherReportForCity(string cityName)
        {
            object cachedReport = weatherCache.Get(cityName);
            if (cachedReport == null)
            {
                WeatherReport cityWeatherReport = await repository.GetLatestWeatherReportForCity(cityName);
                if (cityWeatherReport != null)
                    weatherCache.Add(cityName, cityWeatherReport, cacheItemPolicy);
                return cityWeatherReport;
            }
            else
            {
                return cachedReport as WeatherReport;
            }
        }
    }
}