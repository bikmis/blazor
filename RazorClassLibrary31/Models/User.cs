using System;
using System.ComponentModel.DataAnnotations;

namespace RazorClassLibrary31.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
