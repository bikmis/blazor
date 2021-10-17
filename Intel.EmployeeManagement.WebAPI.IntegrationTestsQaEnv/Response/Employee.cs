using System;

namespace Intel.EmployeeManagement.WebAPI.IntegrationTestsQaEnv.Response
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public int DepartmentID { get; set; }
        public string Gender { get; set; }
    }
}
