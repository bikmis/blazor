using DataLayer31;
using DataLayer31.Entities;
using EmployeeWebAPI31.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeWebAPI31.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [Route("employees")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var response = _dbContext.Employees.Select(e => new EmployeeResponse() 
            {
                ID = e.ID,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Position = e.Position,
                Age = e.Age
            }).ToList();
            return Ok(response);
        }

        [Route("employees/{id}")] //{id} is a variable parameter
        [HttpGet]
        public IActionResult GetEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);
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
                Age = employee.Age
            };

            return Ok(response);
        }

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
                Age = request.Age
            };

            _dbContext.Add(employee);
            _dbContext.SaveChanges();
            return Ok();
        }

        [Route("employees")]
        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeRequest request)
        {
            var employee = _dbContext.Employees.Find(request.ID);
            if ( employee == null)
            {
                return BadRequest("Employee not found.");
            }

            employee.FirstName = request.FirstName;
            employee.MiddleName = request.MiddleName;
            employee.LastName = request.LastName;
            employee.DateOfBirth = request.DateOfBirth;
            employee.Position = request.Position;
            employee.Age = request.Age;

            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return Ok();
        }

        [Route("employees")]
        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return BadRequest("Employee not found.");
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok();
        }
    }

}
