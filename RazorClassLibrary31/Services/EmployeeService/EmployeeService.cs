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
                var employees = await appService.DeserializeToList<Employee>(response); 
                return employees;  
            }
            catch (Exception) {
                
                throw; //if api service or database is down, code execution comes here and exception is rethrown.
            } 
        }

        public async Task<HttpResponseMessage> AddEmployee(Employee employee)
        {
            try
            {
                var response = await httpService.SendAsync(HttpMethod.Post, "api/employees", employee);
                return response; //if api service returns a response with any status code. For Blazor Server, api will return http response with 500 when the database is downed.
            }
            catch (Exception)
            {
                throw; //if api service is down or throws an exception, code execution will arrive here.For web assembly downed database server will throw an exception.
            }
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

        public async Task<HttpResponseMessage> EditEmployee(Employee employee)
        {
            try
            {
                var response = await httpService.SendAsync(HttpMethod.Put, "api/employees", employee);
                return response; //the response (with a status code such as 200, 400, 500 etc. if the db is down, it is 500 (internal server error))
            }
            catch (Exception) {
                throw; //if api service throws an exception or is down.
            }
        }
    }
}