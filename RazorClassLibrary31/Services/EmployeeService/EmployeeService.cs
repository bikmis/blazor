using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.LoginService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using RazorClassLibrary31.Services.TokenService;

namespace RazorClassLibrary31.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        public EmployeeService(HttpClient _httpClient, ITokenService _tokenService)
        {
            httpClient = _httpClient;
            tokenService = _tokenService;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/employees");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.Jwt);
                var response = await httpClient.SendAsync(request);
                var employees = await JsonSerializer.DeserializeAsync<List<Employee>>(response.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return employees;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task AddEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json"); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var response = await httpClient.PostAsync("api/employees", employeeJson);          
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await httpClient.DeleteAsync($"api/employees?id={employeeId}");
        }

        public async Task EditEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("api/employees", employeeJson);
        }
    }
}