using System;
using System.ComponentModel.DataAnnotations;

namespace Razor.Components.Library.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        [Required]
        public string email { get; set; }
    }
}
