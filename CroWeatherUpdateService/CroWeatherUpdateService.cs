using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using WeatherDomainLibrary.Model;
using WeatherDomainLibrary.WeatherRepository;

namespace CroWeatherUpdateService
{
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public partial class CroWeatherUpdateService : ServiceBase
    {

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        private readonly EventLog eventLog;
        private readonly WeatherUpdater weatherUpdater;

        public CroWeatherUpdateService(List<City> listOfCities)
        {
            InitializeComponent();

            eventLog = new EventLog();
            weatherUpdater = new WeatherUpdater(listOfCities);

            if (!EventLog.SourceExists("CroWeatherSource"))
            {
                EventLog.CreateEventSource(
                    "CroWeatherSource", "CroWeatherNewLog");
            }
            eventLog.Source = "CroWeatherSource";
            eventLog.Log = "CroWeatherNewLog";
        }

        private async void OnTimerAsync(object sender, ElapsedEventArgs args)
        {
            await weatherUpdater.FetchAndSaveWeatherForAllCities();
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(ServiceHandle, ref serviceStatus);

            eventLog.WriteEntry(String.Format("+ CroWeatherService started. Time: {0}", DateTime.Now.ToString()));
            Task.WaitAll(weatherUpdater.FetchAndSaveWeatherForAllCities());

            Timer timer = new Timer();
            timer.Interval = 300000; // 5 min
            timer.Elapsed += new ElapsedEventHandler(OnTimerAsync);
            timer.Start();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            // Update the service state to Stop Pending.
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_STOP_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(ServiceHandle, ref serviceStatus);

            eventLog.WriteEntry(String.Format("- CroWeatherService stopped. Time: {0}", DateTime.Now.ToString()));

            // Update the service state to Stopped.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(ServiceHandle, ref serviceStatus);
        }
    }
}
