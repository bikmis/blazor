using Razor.Components.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Razor.Components.Library.Services.EmployeeService
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task AddEmployee(Employee employee);
        public Task DeleteEmployee(int employeeId);
    }
}
