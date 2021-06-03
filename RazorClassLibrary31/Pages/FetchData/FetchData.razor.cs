using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.FetchData
{
    public partial class FetchData
    {
        private WeatherForecast[] forecasts { get; set; }

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
            forecasts = await httpClient.GetFromJsonAsync<WeatherForecast[]>("_content/Intel.EmployeeManagement.RazorClassLibrary/sample-data/weather.json");
        }
    }
}
