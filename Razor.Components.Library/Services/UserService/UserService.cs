using Razor.Components.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace Razor.Components.Library.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var userResponse = await _httpClient.GetStreamAsync("users");
            var users = await JsonSerializer.DeserializeAsync<List<User>>(userResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return users;
        }
    }
}