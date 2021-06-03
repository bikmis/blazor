using System.ComponentModel.DataAnnotations;

namespace Intel.EmployeeManagement.RazorClassLibrary.Validation
{
    public class AgeValidator : ValidationAttribute
    {
        public int MinimumAge { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.Parse(value.ToString()) < MinimumAge)
            {
                return new ValidationResult($"Age cannot be less than {MinimumAge}.", new[] { validationContext.MemberName });
                //return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });//In this one, you need to provide the ErrorMessage = "Age cannot be less than 14." in the model itself.
            }

            return null;
        }

    }
}
