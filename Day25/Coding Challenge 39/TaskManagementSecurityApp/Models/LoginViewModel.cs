using System.ComponentModel.DataAnnotations;

namespace TaskManagementSecurityApp.Models;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
