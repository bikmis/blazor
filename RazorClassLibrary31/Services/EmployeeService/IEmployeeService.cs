using RazorClassLibrary31.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.Employee_Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task AddEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
        Task EditEmployee(Employee employee);
    }
}
