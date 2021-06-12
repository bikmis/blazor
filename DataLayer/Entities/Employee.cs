using System;
using System.ComponentModel.DataAnnotations;

namespace Intel.EmployeeManagement.Data.Entities
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Position { get; set; }
        public int DepartmentID { get; set; }
    }
}
