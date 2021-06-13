using System.ComponentModel.DataAnnotations;

namespace Intel.EmployeeManagement.RazorClassLibrary.Validation
{
    public class DepartmentIDValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.Parse(value.ToString()) == 0)
            {
                return new ValidationResult($"Department ID is required.", new[] { validationContext.MemberName });
                //return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });//In this one, you need to provide the ErrorMessage = "Department ID is required." in the model itself.
            }

            return null;
        }

    }
}
