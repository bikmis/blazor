﻿using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service
{
    public class AppStoreService : IAppStoreService
    {
        private User _user = new User();
        private string _accessToken;

        public User User { get => _user; set => _user = value; }

        public string AccessToken { get => _accessToken; set => _accessToken = value; }

        public async Task<T> DeserializeToType<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return default(T); //return default value of generic type.
            }
            return await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<T>> DeserializeToListOfType<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<List<T>>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public StringContent SerializeToString(object data)
        {
            //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var stringContent = data != null ? new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json") : null;
            return stringContent;
        }
    }
}
