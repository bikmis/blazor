﻿using EmployeeBlazor.Models;
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
            return await JsonSerializer.DeserializeAsync<List<Employee>>
                (await _httpClient.GetStreamAsync("https://localhost:44327/api/employees"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

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