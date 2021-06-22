using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
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
        private readonly IAppStateService appStateService;

        public EmployeeService(IHttpService _httpService, IAppStateService _appStateService)
        {
            httpService = _httpService;
            appStateService = _appStateService;
        }

        public async Task<HttpResponseMessage> GetEmployees()
        {
            try
            {
                return await httpService.SendAsync(HttpMethod.Get, "api/employees", null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> AddEmployee(Employee employee)
        {
            try
            {
                return await httpService.SendAsync(HttpMethod.Post, "api/employees", employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteEmployee(int employeeId)
        {
            try
            {
                return await httpService.SendAsync(HttpMethod.Delete, $"api/employees?id={employeeId}", null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> EditEmployee(Employee employee)
        {
            try
            {
                return await httpService.SendAsync(HttpMethod.Put, "api/employees", employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}