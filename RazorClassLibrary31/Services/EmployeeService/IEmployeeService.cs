﻿using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task AddEmployee(Employee employee);
        Task<HttpResponseMessage> DeleteEmployee(int employeeId);
        Task EditEmployee(Employee employee);
    }
}
