using Microsoft.AspNetCore.Identity;
using OnlineBookstoreApp.Models;

namespace OnlineBookstoreApp.Services;

public class UserService
{
    private readonly PasswordHasher<AppUser> _hasher = new();
    private readonly List<AppUser> _users;

    public UserService()
    {
        var admin = new AppUser
        {
            UserName = "admin",
            Email = "admin@bookstore.local",
            Role = "Admin"
        };
        admin.PasswordHash = _hasher.HashPassword(admin, "Admin@123");

        _users =
        [
            admin
        ];
    }

    public bool UserNameExists(string userName) =>
        _users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

    public AppUser Register(RegisterInputModel input)
    {
        var user = new AppUser
        {
            UserName = input.UserName.Trim(),
            Email = input.Email.Trim(),
            Role = "Customer"
        };

        user.PasswordHash = _hasher.HashPassword(user, input.Password);
        _users.Add(user);
        return user;
    }

    public AppUser? ValidateCredentials(string userName, string password)
    {
        var user = _users.FirstOrDefault(item => item.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));
        if (user is null)
        {
            return null;
        }

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success ? user : null;
    }
}
