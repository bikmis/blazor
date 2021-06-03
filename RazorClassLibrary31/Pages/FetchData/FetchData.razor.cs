using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.Personnel.RazorClassLibrary.Models;
using Intel.Personnel.RazorClassLibrary.Services.Authentication_Service;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Pages.FetchData
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
            forecasts = await httpClient.GetFromJsonAsync<WeatherForecast[]>("_content/Intel.Personnel.RazorClassLibrary/sample-data/weather.json");
        }
    }
}
