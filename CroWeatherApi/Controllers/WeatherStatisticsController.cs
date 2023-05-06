using CroWeatherApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherDomainLibrary.Model;
using WeatherDomainLibrary.WeatherRepository;

namespace CroWeatherApi.Controllers
{
    public class WeatherStatisticsController : ApiController
    {
        private readonly StatisticsService statisticsService = new StatisticsService();

        [HttpGet]
        [ResponseType(typeof(double))]
        [Route("api/stats/{cityName}/avg-temp")]
        /// Date example "2023-05-06 13:26"
        public IHttpActionResult GetAverageTemperature(string cityName, string periodStart, string periodEnd)
        {
            try
            {
                double averageTemp = statisticsService.GetAverageTemperature(cityName, periodStart, periodEnd);
                if (averageTemp == null)
                {
                    return NotFound();
                }
                return Ok(averageTemp);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }

        [HttpGet]
        [ResponseType(typeof(double))]
        [Route("api/stats/{cityName}/top-oscillation")]
        public IHttpActionResult GetMaximumTemperatureOscillation(string cityName, string periodStart, string periodEnd)
        {
            return InternalServerError(new NotImplementedException());
            // TODO
            // object oscillation = statisticsService.GetMaximumTemperatureOscillation(cityName, periodStart, periodEnd);
        }


    }
}
