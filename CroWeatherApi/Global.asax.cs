using System.Data.SqlClient;
using System.Web.Http;
using WeatherDomainLibrary.WeatherRepository;

namespace CroWeatherApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
