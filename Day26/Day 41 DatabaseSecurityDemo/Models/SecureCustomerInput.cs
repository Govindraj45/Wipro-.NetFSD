using System.ComponentModel.DataAnnotations;

namespace DatabaseSecurityDemo.Models;

public class SecureCustomerInput
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string SensitiveNote { get; set; } = string.Empty;
}
