using EmployeeBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace EmployeeBlazor.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employeeResponse = await _httpClient.GetStreamAsync("api/employees");
            var employees = await JsonSerializer.DeserializeAsync<List<Employee>>(employeeResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return employees;
        }
    }
}