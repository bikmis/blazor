using Razor.Components.Library.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Razor.Components.Library.Services.EmployeeService
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
            var employeesJson = await _httpClient.GetStreamAsync("api/employees");
            var employees = await JsonSerializer.DeserializeAsync<List<Employee>>(employeesJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return employees;
        }

        public async Task AddEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json"); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            await _httpClient.PostAsync("api/employees", employeeJson);           
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _httpClient.DeleteAsync($"api/employees?id={employeeId}");
        }
    }
}