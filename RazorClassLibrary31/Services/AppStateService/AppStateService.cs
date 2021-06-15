using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service
{
    public class AppStateService : IAppStateService
    {
        private User user = new User();
        private string accessToken;

        public User User
        {
            get => user;
            set
            {
                user = value;
                notifyStateChanged();
            }
        }

        public string AccessToken
        {
            get => accessToken;
            set { 
                accessToken = value;
                notifyStateChanged();
            }
        }

        public event Action OnChange;

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

        private void notifyStateChanged() {
            OnChange?.Invoke();
        }

    }
}
