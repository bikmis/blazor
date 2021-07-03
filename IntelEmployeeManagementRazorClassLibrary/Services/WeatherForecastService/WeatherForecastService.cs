using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.WeatherForecast_Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private HttpClient httpClient { get; set; }

        public WeatherForecastService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<WeatherForecast[]> ListWeatherForecast()
        {
            var forecasts = await httpClient.GetFromJsonAsync<WeatherForecast[]>("_content/Intel.EmployeeManagement.RazorClassLibrary/sample-data/weather.json");
            return forecasts;
        }
    }
}
