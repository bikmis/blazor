using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeBlazor.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
