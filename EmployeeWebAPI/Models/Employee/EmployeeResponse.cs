using System;

namespace Intel.EmployeeManagement.WebAPI.Models.Employee
{
    public class EmployeeResponse
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public int DepartmentNumber { get; set; }
    }
}
