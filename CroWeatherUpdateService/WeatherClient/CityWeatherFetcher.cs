using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherDomainLibrary.Model;

namespace CroWeatherUpdateService.WeatherClient
{
    class CityWeatherFetcher
    {
        private readonly HttpClient client = new HttpClient();
        private readonly String weatherApiKey = "57f95bfc61602811e7ad61d65bbc773d";
        private readonly String weatherApiBaseUrl = "https://api.openweathermap.org/data/2.5/group";

        public static readonly int maxCityIdsPerCall = 20;

        public async Task<List<WeatherReport>> FetchCitiesWeatherData(List<City> cities)
        {
            List<String> citiesIds = cities.ConvertAll<string>(city => city.Id.ToString());
            String citiesIdsParameter = String.Join(",", citiesIds);
            String weatherApiUrl = String.Format("{0}?units=metric&id={1}&appid={2}", weatherApiBaseUrl, citiesIdsParameter, weatherApiKey);
           
            HttpResponseMessage response = (await client.GetAsync(weatherApiUrl));
            if (response.IsSuccessStatusCode)
            {
                var jstring = await response.Content.ReadAsStringAsync();
                var citiesWeather = JsonSerializer.Deserialize<WeatherResultsWrapper>(jstring, JsonSerializerCustomOptions.SnakeCaseJsonSerializerOptions);
                return citiesWeather.List;
            }
            else
            {
                Console.WriteLine(String.Format("Fetching error, response code: {0}", response.StatusCode));
                return new List<WeatherReport> { };
            }
        }
    }
}
