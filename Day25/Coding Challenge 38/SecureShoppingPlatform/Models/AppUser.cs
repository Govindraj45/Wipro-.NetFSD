using System.ComponentModel.DataAnnotations;

namespace SecureShoppingPlatform.Models;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(60)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Customer";
    public int FailedLoginCount { get; set; }
    public DateTimeOffset? LockedUntilUtc { get; set; }
}
