using System.ComponentModel.DataAnnotations;
using FeedbackValidationPortal.Validation;

namespace FeedbackValidationPortal.Models;

[PasswordMatch]
public class RegistrationInputModel
{
    [Required]
    [StringLength(40, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
}
