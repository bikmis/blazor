using DataLayer;
using DataLayer.Entities;
using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeeWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeDbContext _dbContext;

        public EmployeeController()
        {
            _dbContext = new EmployeeDbContext();
        }

        [Route("employees")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _dbContext.Employees.Select(e => new EmployeeDto()
            {
                ID = e.ID,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Position = e.Position
            }).ToList();
            return Ok(employees);
        }

        [Route("employee/{id}")] //{id} is a variable parameter
        [HttpGet]
        public IActionResult GetEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var employeeDto = new EmployeeDto()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position
            };

            return Ok(employeeDto);
        }

        [Route("employee/add")]
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                MiddleName = employeeDto.MiddleName,
                LastName = employeeDto.LastName,
                Position = employeeDto.Position,
                DateOfBirth = employeeDto.DateOfBirth
            };

            _dbContext.Add(employee);
            _dbContext.SaveChanges();
            return Ok();
        }

        [Route("employee/delete")]
        [HttpPost]
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

        [Route("employee/update")]
        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeDto employeeDto)
        {
            var employee = _dbContext.Employees.Find(employeeDto.ID);
            if ( employee == null)
            {
                return BadRequest("Employee not found.");
            }

            employee.FirstName = employeeDto.FirstName;
            employee.MiddleName = employeeDto.MiddleName;
            employee.LastName = employeeDto.LastName;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            employee.Position = employeeDto.Position;

            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
