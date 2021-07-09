using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.BlazorClient.UnitTests.Services
{
    public class MockAppStateService : IAppStateService
    {
        private AlertPopUp alertPopUp { get; set; } = new AlertPopUp() { Message = null, IsHidden = true, Color = null };

        public User User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessToken { get => null; set => value = null; }
        public DateTime? Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AlertPopUp AlertPopUp
        {
            get => alertPopUp;
            set
            {
                alertPopUp = value;
                notifyStateChanged();
            }
        }

        public event Action OnChange;

        public async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<T>> DeserializeToList<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<List<T>>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public HttpContent Serialize(object data)
        {
            var stringContent = data != null ? new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json") : null;
            return stringContent;
        }

        private void notifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
