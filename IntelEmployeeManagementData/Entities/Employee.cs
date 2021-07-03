using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "char(1)")]
        public char Gender { get; set; }
    }
}
