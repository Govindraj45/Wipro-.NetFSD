using System.ComponentModel.DataAnnotations;

namespace OnlineBookstoreApp.Models;

public class LoginInputModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
