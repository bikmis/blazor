﻿using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpService httpService;
        private readonly IAppStoreService appStoreService;

        public EmployeeService(HttpClient _httpClient, IHttpService _httpService, IAppStoreService _appStoreService)
        {
            httpClient = _httpClient;
            httpService = _httpService;
            appStoreService = _appStoreService;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var response = await httpService.SendAsync(httpClient, HttpMethod.Get, "api/employees", null);
                var employees = await appStoreService.DeserializeToListOfType<Employee>(response);
                return employees;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task AddEmployee(Employee employee)
        {
            await httpService.SendAsync(httpClient, HttpMethod.Post, "api/employees", employee);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await httpService.SendAsync(httpClient, HttpMethod.Delete, $"api/employees?id={employeeId}", null);
        }

        public async Task EditEmployee(Employee employee)
        {
            await httpService.SendAsync(httpClient, HttpMethod.Put, "api/employees", employee);
        }
    }
}