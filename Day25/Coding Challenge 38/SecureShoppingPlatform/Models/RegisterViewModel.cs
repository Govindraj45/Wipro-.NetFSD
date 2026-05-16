using System.ComponentModel.DataAnnotations;

namespace SecureShoppingPlatform.Models;

public class RegisterViewModel
{
    [Required]
    [StringLength(60)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
        ErrorMessage = "Password must be 8+ chars with uppercase, number, and special character.")]
    public string Password { get; set; } = string.Empty;
}
