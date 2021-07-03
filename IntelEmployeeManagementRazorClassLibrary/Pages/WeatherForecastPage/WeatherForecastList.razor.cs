using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.WeatherForecast_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.WeatherForecastPage
{
    public partial class WeatherForecastList
    {
        private WeatherForecast[] forecasts { get; set; }

        [Inject]
        private IWeatherForecastService weatherForecastService { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ((IAuthenticationService)authenticationService).GuardRoute();
            forecasts = await weatherForecastService.ListWeatherForecast(); //await httpClient.GetFromJsonAsync<WeatherForecast[]>("_content/Intel.EmployeeManagement.RazorClassLibrary/sample-data/weather.json");
        }
    }
}
