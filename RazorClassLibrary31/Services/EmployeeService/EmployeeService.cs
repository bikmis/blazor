using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.HttpService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpService httpService;
        public EmployeeService(HttpClient _httpClient, IHttpService _httpService)
        {
            httpClient = _httpClient;
            httpService = _httpService;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var response = await httpService.SendAsync(httpClient, HttpMethod.Get, "api/employees");
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
            var data = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json"); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            await httpService.SendAsync(httpClient, HttpMethod.Post, "api/employees", data);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await httpService.SendAsync(httpClient, HttpMethod.Delete, $"api/employees?id={employeeId}");
        }

        public async Task EditEmployee(Employee employee)
        {
            var data = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            await httpService.SendAsync(httpClient, HttpMethod.Put, "api/employees", data);
        }
    }
}