using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherDomainLibrary.Model;
using CroWeatherApi.Repository;

namespace CroWeatherApi.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly CachedWeatherRepository repository = CachedWeatherRepository.Instance;

        [HttpGet]
        [ResponseType(typeof(WeatherReport))]
        [Route("api/weather/{weatherReportId}")]
        public async Task<IHttpActionResult> WeatherReportForId(int weatherReportId)
        {
            WeatherReport weatherReport = await repository.GetWeatherReportForId(weatherReportId);
            if (weatherReport == null)
            {
                return NotFound();
            }
            return Ok(weatherReport);
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