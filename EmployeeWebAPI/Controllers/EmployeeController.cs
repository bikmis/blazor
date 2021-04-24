using DataLayer;
using DataLayer.Entities;
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
            var employees = _dbContext.Employees.ToList();
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
            return Ok(employee);
        }

        [Route("employee/add")]
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
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
        public IActionResult UpdateEmployee(Employee employee)
        {
            var dbEmployee = _dbContext.Employees.Find(employee.ID);
            if ( dbEmployee == null)
            {
                return BadRequest("Employee not found.");
            }

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.MiddleName = employee.MiddleName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.DateOfBirth = employee.DateOfBirth;
            dbEmployee.Position = employee.Position;

            _dbContext.Employees.Update(dbEmployee);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
