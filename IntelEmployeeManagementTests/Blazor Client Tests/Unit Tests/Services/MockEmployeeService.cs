﻿using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests.Services
{
    public class MockEmployeeService : IEmployeeService
    {
        List<Employee> employees = new List<Employee>() {
            new Employee() { ID = 1, FirstName = "Jack", MiddleName="", LastName="Smith", DepartmentID = 1, Gender="M", Position="Sales Rep", DateOfBirth=DateTime.Now},
            new Employee() { ID = 2, FirstName = "Mike", MiddleName="", LastName="Johnson", DepartmentID =5, Gender="M", Position="CEO", DateOfBirth=DateTime.Now},
            new Employee() { ID = 3, FirstName = "Sophia", MiddleName="Rose", LastName="Miller", DepartmentID =10, Gender="F", Position="Programmer", DateOfBirth=DateTime.Now }
        };

        public Task<HttpResponseMessage> AddEmployee(Employee employee)
        {
            employee.ID = 4;
            employees.Add(employee);
            HttpContent content = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage() { Content = content, StatusCode = HttpStatusCode.OK };
            return Task.FromResult(response);
        }

        public Task<HttpResponseMessage> DeleteEmployee(int employeeId)
        {
            var employee = employees.Find(x => x.ID == employeeId);
            var index = employees.IndexOf(employee);
            employees.RemoveAt(index);
            HttpContent content = new StringContent(JsonSerializer.Serialize(employees), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage() { Content = content, StatusCode = HttpStatusCode.OK };
            return Task.FromResult(response);
        }

        public Task<HttpResponseMessage> EditEmployee(Employee employee)
        {
            var emp = employees.Find(x => x.ID == employee.ID);
            var index = employees.IndexOf(emp);
            employees.RemoveAt(index);
            employee.MiddleName = "Thomas";
            employees.Add(employee);
            HttpContent content = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage() { Content = content, StatusCode = HttpStatusCode.OK };
            return Task.FromResult(response);
        }

        public Task<HttpResponseMessage> GetEmployees()
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(employees), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage() { Content = content, StatusCode = HttpStatusCode.OK };
            return Task.FromResult(response);
        }
    }
}