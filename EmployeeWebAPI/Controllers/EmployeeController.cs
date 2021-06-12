using Intel.EmployeeManagement.Data;
using Intel.EmployeeManagement.Data.Entities;
using Intel.EmployeeManagement.WebAPI.Models.Employee;
using Intel.EmployeeManagement.WebAPI.Services.Database_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Intel.EmployeeManagement.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDatabaseService databaseService;
        public EmployeeController(IDatabaseService _databaseService)
        {
            databaseService = _databaseService;
        }

        [Authorize]
        [Route("employees")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var response = databaseService.EmployeeDbContext.Employees.Select(e => new EmployeeResponse() 
            {
                ID = e.ID,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Position = e.Position,
                DepartmentID = e.DepartmentID,
                Gender = e.Gender
            }).ToList();
            return Ok(response);
        }

        [Authorize]
        [Route("employees/{id}")] //{id} is a variable parameter
        [HttpGet]
        public IActionResult GetEmployee(int id)
        {
            var employee = databaseService.EmployeeDbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var response = new EmployeeResponse()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position,
                DepartmentID = employee.DepartmentID,
                Gender = employee.Gender
            };

            return Ok(response);
        }

        [Authorize]
        [Route("employees")]
        [HttpPost]
        public IActionResult AddEmployee(EmployeeRequest request)
        {
            Employee employee = new Employee()
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Position = request.Position,
                DateOfBirth = request.DateOfBirth,
                DepartmentID = request.DepartmentID,
                Gender = request.Gender
            };

            databaseService.EmployeeDbContext.Add(employee);
            databaseService.EmployeeDbContext.SaveChanges();
            return Ok();
        }

        [Authorize]
        [Route("employees")]
        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeRequest request)
        {
            var employee = databaseService.EmployeeDbContext.Employees.Find(request.ID);
            if ( employee == null)
            {
                return BadRequest("Employee not found.");
            }

            employee.FirstName = request.FirstName;
            employee.MiddleName = request.MiddleName;
            employee.LastName = request.LastName;
            employee.DateOfBirth = request.DateOfBirth;
            employee.Position = request.Position;
            employee.DepartmentID = request.DepartmentID;
            employee.Gender = request.Gender;

            databaseService.EmployeeDbContext.Employees.Update(employee);
            databaseService.EmployeeDbContext.SaveChanges();
            return Ok();
        }

        [Authorize]
        [Route("employees")]
        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = databaseService.EmployeeDbContext.Employees.Find(id);
            if (employee == null)
            {
                return BadRequest("Employee not found.");
            }
            databaseService.EmployeeDbContext.Employees.Remove(employee);
            databaseService.EmployeeDbContext.SaveChanges();
            return Ok();
        }
    }

}
