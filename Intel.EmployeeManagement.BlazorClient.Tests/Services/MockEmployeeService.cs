﻿using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.BlazorClient.Tests.Services
{
    public class MockEmployeeService : IEmployeeService
    {
        List<Employee> employees = new List<Employee>() {
            new Employee() { ID = 1, FirstName = "Jack", MiddleName="", LastName="Smith", DepartmentID = 2, Gender="M", Position="Sales Rep", DateOfBirth=DateTime.Now},
            new Employee() { ID = 1, FirstName = "Mike", MiddleName="", LastName="Johnson", DepartmentID = 2, Gender="M", Position="CEO", DateOfBirth=DateTime.Now},
            new Employee() { ID = 1, FirstName = "Sophia", MiddleName="Rose", LastName="Miller", DepartmentID = 2, Gender="F", Position="Programmer", DateOfBirth=DateTime.Now}
        };

        public Task<HttpResponseMessage> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetEmployees()
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(employees), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage() { Content = content, StatusCode = HttpStatusCode.OK };
            return Task.FromResult(response);
        }
    }
}
