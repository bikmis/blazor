using System.ComponentModel.DataAnnotations;

namespace Intel.EmployeeManagement.RazorClassLibrary.Validation
{
    public class DepartmentIDValidator : ValidationAttribute
    {
        public int MinimumDepartmentID { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.Parse(value.ToString()) < MinimumDepartmentID)
            {
                return new ValidationResult($"Department ID cannot be less than {MinimumDepartmentID}.", new[] { validationContext.MemberName });
                //return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });//In this one, you need to provide the ErrorMessage = "Department ID cannot be less than 14." in the model itself.
            }

            return null;
        }

    }
}
