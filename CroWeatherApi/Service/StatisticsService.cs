using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherDomainLibrary.WeatherRepository;

namespace CroWeatherApi.Service
{
    public class StatisticsService
    {
        private readonly WeatherReportRepository repository = new WeatherReportRepository();

        public double GetAverageTemperature(string cityName, string periodStart, string periodEnd)
        {
            long periodStartUnixTimeStamp = unixTimeStamp(periodStart);
            long periodEndUnixTimeStamp = unixTimeStamp(periodEnd);
            return repository.GetWeatherReportsForCity(cityName)
                .Where(x => periodStartUnixTimeStamp < x.Dt && x.Dt < periodEndUnixTimeStamp)
                .Average(x => x.Main.Temp);
        }
        
        public object GetMaximumTemperatureOscillation(string cityName, string periodStart, string periodEnd)
        {
            // TODO: Connect to db, problems with raw sql
            long periodStartUnixTimeStamp = DateTime.Parse(periodStart).ToUniversalTime().Ticks;
            long periodEndUnixTimeStamp = DateTime.Parse(periodEnd).ToUniversalTime().Ticks;
            var sqlCode = String.Format(
                  $@"SELECT TOP 1 [WeatherReportId]
                      ,[SysId]
                      ,[WeatherDB].[dbo].[WeatherReports].[MainWeatherInformationId]
                      ,[Visibility]
                      ,[Dt]
                      ,[Name]
	                  ,[WeatherDB].[dbo].[MainWeatherInformations].Temp
	                  ,LAG([WeatherDB].[dbo].[MainWeatherInformations].Temp) over (order by [WeatherDB].[dbo].[MainWeatherInformations].Temp) AS previous_temp
	                  ,ABS([WeatherDB].[dbo].[MainWeatherInformations].Temp - LAG([WeatherDB].[dbo].[MainWeatherInformations].Temp) over ( order by [WeatherDB].[dbo].[MainWeatherInformations].Temp)) as temp_dif
                  FROM [WeatherDB].[dbo].[WeatherReports]
                  INNER JOIN [WeatherDB].[dbo].[MainWeatherInformations] ON [WeatherDB].[dbo].[MainWeatherInformations].MainWeatherInformationId = [WeatherDB].[dbo].[WeatherReports].MainWeatherInformationId
                  WHERE [Name] = '{cityName}' and Dt between {periodStartUnixTimeStamp} and {periodEndUnixTimeStamp}
                  order by temp_dif DESC;", cityName);
            return repository.RunSqlQuery(sqlCode);
        }

        private int unixTimeStamp(string time)
        {
            var dateTime = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).ToUniversalTime();
            return ((int)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
    }
}