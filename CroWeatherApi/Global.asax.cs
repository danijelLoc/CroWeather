﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

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