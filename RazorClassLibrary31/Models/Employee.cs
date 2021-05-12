using System;
using System.ComponentModel.DataAnnotations;

namespace RazorClassLibrary31.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Position { get; set; }
        public int Age { get; set; }
    }
}
