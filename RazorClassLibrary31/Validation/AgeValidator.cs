using System.ComponentModel.DataAnnotations;

namespace Intel.EmployeeManagement.RazorClassLibrary.Validation
{
    public class DepartmentNumberValidator : ValidationAttribute
    {
        public int MinimumDepartmentNumber { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.Parse(value.ToString()) < MinimumDepartmentNumber)
            {
                return new ValidationResult($"Department Number cannot be less than {MinimumDepartmentNumber}.", new[] { validationContext.MemberName });
                //return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });//In this one, you need to provide the ErrorMessage = "Department Number cannot be less than 14." in the model itself.
            }

            return null;
        }

    }
}
