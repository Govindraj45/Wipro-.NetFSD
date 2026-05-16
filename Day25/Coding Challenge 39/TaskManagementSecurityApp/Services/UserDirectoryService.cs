using Microsoft.AspNetCore.Identity;
using TaskManagementSecurityApp.Models;

namespace TaskManagementSecurityApp.Services;

public class UserDirectoryService
{
    private readonly PasswordHasher<AppUser> _hasher = new();
    private readonly List<AppUser> _users;

    public UserDirectoryService()
    {
        var admin = new AppUser { UserName = "admin", Role = "Admin", CanEditTask = true };
        admin.PasswordHash = _hasher.HashPassword(admin, "Admin@123");

        var user = new AppUser { UserName = "member", Role = "User", CanEditTask = true };
        user.PasswordHash = _hasher.HashPassword(user, "Member@123");

        _users = [admin, user];
    }

    public bool Exists(string userName) =>
        _users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

    public AppUser Register(RegisterViewModel model)
    {
        var user = new AppUser
        {
            UserName = model.UserName.Trim(),
            Role = "User",
            CanEditTask = true
        };
        user.PasswordHash = _hasher.HashPassword(user, model.Password);
        _users.Add(user);
        return user;
    }

    public AppUser? Validate(string userName, string password)
    {
        var user = _users.FirstOrDefault(item => item.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));
        if (user is null)
        {
            return null;
        }

        return _hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success
            ? user
            : null;
    }
}
