using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RazorClassLibrary31.Validation
{
    public class AgeValidator : ValidationAttribute
    {
        public int MinimumAge { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.Parse(value.ToString()) < MinimumAge)
            {
                return new ValidationResult($"Age cannot be less than {MinimumAge}.", new[] { validationContext.MemberName });
            }

            return null; // base.IsValid(value, validationContext);
        }

    }
}
