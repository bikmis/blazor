using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service
{
    public class PhotoService : IPhotoService
    {
        private HttpClient httpClient { get; set; }

        public PhotoService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            var photos = (await httpClient.GetFromJsonAsync<List<Photo>>("photos", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
            return photos;
        }
    }
}
