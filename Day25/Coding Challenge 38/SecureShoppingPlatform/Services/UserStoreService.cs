using Microsoft.AspNetCore.Identity;
using SecureShoppingPlatform.Models;

namespace SecureShoppingPlatform.Services;

public class UserStoreService
{
    private readonly PasswordHasher<AppUser> _hasher = new();
    private readonly List<AppUser> _users;

    public UserStoreService()
    {
        var admin = new AppUser
        {
            UserName = "admin",
            Email = "admin@secure-shop.local",
            Role = "Admin"
        };
        admin.PasswordHash = _hasher.HashPassword(admin, "Admin@123");

        _users = [admin];
    }

    public bool Exists(string userName) =>
        _users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

    public AppUser Register(RegisterViewModel input)
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

    public (AppUser? User, string? Error) Validate(string userName, string password)
    {
        var user = _users.FirstOrDefault(item => item.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));
        if (user is null)
        {
            return (null, "Invalid username or password.");
        }

        if (user.LockedUntilUtc is not null && user.LockedUntilUtc > DateTimeOffset.UtcNow)
        {
            return (null, "Account temporarily locked. Please wait and try again.");
        }

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Success)
        {
            user.FailedLoginCount = 0;
            user.LockedUntilUtc = null;
            return (user, null);
        }

        user.FailedLoginCount++;
        if (user.FailedLoginCount >= 3)
        {
            user.LockedUntilUtc = DateTimeOffset.UtcNow.AddMinutes(1);
            user.FailedLoginCount = 0;
        }

        return (null, "Invalid username or password.");
    }
}
