using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage
{
    public partial class TestExample
    {
        [Inject]
        HttpClient httpClient { get; set; }

        [Inject]
        private IConfiguration configuration { get; set; }

        public IEnumerable<Photo> photos { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await getPhotos();
        }

        private async Task getPhotos() {
            photos = (await httpClient.GetFromJsonAsync<List<Photo>>(configuration["PhotoServiceBaseAddress"] + "photos",
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
        }
    }


}
