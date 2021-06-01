using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.Authentication_Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.FetchData
{
    public partial class FetchData : ComponentBase
    {
        private WeatherForecast[] forecasts { get; set; }

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
            forecasts = await httpClient.GetFromJsonAsync<WeatherForecast[]>("_content/RazorClassLibrary31/sample-data/weather.json");
        }
    }
}
