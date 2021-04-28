using EmployeeBlazor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = _httpClient.GetAsync("https://localhost:44327/api/employees");
            var response = await employees.Result.Content.ReadAsStringAsync();
            var employeeList = JsonConvert.DeserializeObject<List<Employee>>(response);        
            return employeeList;
        }
    }
}
