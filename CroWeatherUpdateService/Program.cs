﻿using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace CroWeatherUpdateService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (System.Diagnostics.Debugger.IsAttached && args.Contains("--test"))
            {
                Console.WriteLine("<<<<<<<<<<<<<<< One Service call simulation >>>>>>>>>>>>>>>>>>");
                Task.WaitAll(new WeatherUpdater(CroCityList.ListOfCitiesInCroatia()).FetchAndSaveWeatherForAllCities());
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new CroWeatherUpdateService(CroCityList.ListOfCitiesInCroatia())
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
