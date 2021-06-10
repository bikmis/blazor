using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.App_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpService httpService;
        private readonly IAppService appService;

        public EmployeeService(IHttpService _httpService, IAppService _appService)
        {
            httpService = _httpService;
            appService = _appService;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try {
                var response = await httpService.SendAsync(HttpMethod.Get, "api/employees", null);
                var employees = await appService.DeserializeToList<Employee>(response); //if database service is down, the execution comes here. employees will be null.
                return employees;  
            }
            catch (Exception) {
                
                throw; //if api service is down, the execution comes here and exception is thrown.
            } 
        }

        public async Task AddEmployee(Employee employee)
        {
            await httpService.SendAsync(HttpMethod.Post, "api/employees", employee);
        }

        public async Task<HttpResponseMessage> DeleteEmployee(int employeeId)
        {
            try {
                var response = await httpService.SendAsync(HttpMethod.Delete, $"api/employees?id={employeeId}", null);
                return response;
            }
            catch (Exception) {
                throw;
            }
        }

        public async Task EditEmployee(Employee employee)
        {
            await httpService.SendAsync(HttpMethod.Put, "api/employees", employee);
        }
    }
}