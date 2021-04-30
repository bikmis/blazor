using EmployeeBlazor.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task AddEmployee(Employee employee)
        {
            var serializedEmployee = JsonSerializer.Serialize(employee);
            var stringContent = new StringContent(serializedEmployee, UnicodeEncoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/employees", stringContent);           
        }
    }
}