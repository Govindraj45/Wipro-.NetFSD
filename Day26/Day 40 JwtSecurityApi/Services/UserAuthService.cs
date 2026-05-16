using JwtSecurityApi.Models;

namespace JwtSecurityApi.Services;

public class UserAuthService
{
    private readonly List<AppUser> _users =
    [
        new AppUser { UserName = "admin", Password = "Admin@123", Role = "Admin" },
        new AppUser { UserName = "member", Password = "Member@123", Role = "User" }
    ];

    public AppUser? Validate(LoginRequest request)
    {
        return _users.FirstOrDefault(user =>
            user.UserName.Equals(request.UserName.Trim(), StringComparison.OrdinalIgnoreCase) &&
            user.Password == request.Password);
    }
}
