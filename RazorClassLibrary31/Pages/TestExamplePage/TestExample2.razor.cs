using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage
{
    public partial class TestExample2
    {
        [Inject]
        HttpClient httpClient { get; set; }

        [Inject]
        private IConfiguration configuration { get; set; }

        [Inject]
        private IPhotoService photoService { get; set; }

        private bool takingLong { get; set; }

        public IEnumerable<Photo> photos { get; set; } = new List<Photo>();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private async Task getPhotos_1()
        {
            takingLong = true;
            photos = await photoService.GetPhotos();
            takingLong = false;
        }

        private async Task getPhotos_2()
        {
            takingLong = true;
            photos = await httpClient.GetFromJsonAsync<List<Photo>>(configuration["PhotoServiceBaseAddress"] + "photos",
                     new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            takingLong = false;
        }

        private void clearPhotos()
        {
            photos = new List<Photo>();
        }
    }
}
