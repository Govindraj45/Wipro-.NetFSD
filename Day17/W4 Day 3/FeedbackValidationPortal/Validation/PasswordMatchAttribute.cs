using System.ComponentModel.DataAnnotations;
using FeedbackValidationPortal.Models;

namespace FeedbackValidationPortal.Validation;

public class PasswordMatchAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not RegistrationInputModel input)
        {
            return ValidationResult.Success;
        }

        return input.Password == input.ConfirmPassword
            ? ValidationResult.Success
            : new ValidationResult("Password and Confirm Password must match.", [nameof(RegistrationInputModel.ConfirmPassword)]);
    }
}
