using EmployeeBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace EmployeeBlazor.Services
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
            return await JsonSerializer.DeserializeAsync<List<User>>
                (await _httpClient.GetStreamAsync("https://jsonplaceholder.typicode.com/users"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            /*
                var employees = new List<Employee>() { new Employee() { FirstName = "Jack" } };
                return employees;
            */

            /*   
                var employees = _httpClient.GetAsync("api/employees");
                var response = await employees.Result.Content.ReadAsStringAsync();
                var employeeList = JsonConvert.DeserializeObject<List<Employee>>(response);        
                return employeeList;
            */
        }
    }
}