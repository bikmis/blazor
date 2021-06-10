using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.App_Service
{
    public class AppService : IAppService
    {
        private User _user = new User();
        private string _accessToken;

        public User User { get => _user; set => _user = value; }

        public string AccessToken { get => _accessToken; set => _accessToken = value; }

        public async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            if (response == null || !response.IsSuccessStatusCode)
            {
                return default(T); //return default value of generic type.
            }
            return await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<T>> DeserializeToList<T>(HttpResponseMessage response)
        {
            if (response == null || !response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<List<T>>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public HttpContent Serialize(object data)
        {
            //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            //StringContent provides HttpContent based on a string
            var stringContent = data != null ? new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json") : null;
            return stringContent;
        }
    }
}
