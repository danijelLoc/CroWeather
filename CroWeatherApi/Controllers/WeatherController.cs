using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherDomainLibrary.Model;
using WeatherDomainLibrary.WeatherRepository;

namespace CroWeatherApi.Controllers
{
    public class WeatherController : ApiController
    {
        private WeatherReportRepository repository = new WeatherReportRepository();

        [HttpGet]
        [Route("api/weather/all")]
        public IQueryable<WeatherReport> AllWeatherReports()
        {
            return repository.GetWeatherReports();
        }

        [HttpGet]
        [ResponseType(typeof(WeatherReport))]
        [Route("api/weather/{weatherReportId}")]
        public async Task<IHttpActionResult> WeatherReportForId(int weatherReportId)
        {
            WeatherReport weatherReport = await repository.GetWeatherReport(weatherReportId);
            if (weatherReport == null)
            {
                return NotFound();
            }

            return Ok(weatherReport);
        }

        [HttpGet]
        [ResponseType(typeof(List<WeatherReport>))]
        [Route("api/weather/city/{cityName}/all")]
        public async Task<IHttpActionResult> WeatherReportsForCity(string cityName)
        {
            List<WeatherReport> cityWeatherReports = await repository.GetWeatherReportsForCity(cityName);
            if (cityWeatherReports == null)
            {
                return NotFound();
            }

            return Ok(cityWeatherReports);
        }

        [HttpGet]
        [ResponseType(typeof(WeatherReport))]
        [Route("api/weather/city/{cityName}")]
        public async Task<IHttpActionResult> LatestWeatherReportForCity(string cityName)
        {
            WeatherReport cityWeatherReport = await repository.GetLatestWeatherReportForCity(cityName);
            if (cityWeatherReport == null)
            {
                return NotFound();
            }

            return Ok(cityWeatherReport);
        }
    }
}