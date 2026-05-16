using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtSecurityApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtSecurityApi.Services;

public class TokenService(IConfiguration configuration)
{
    public string CreateToken(AppUser user)
    {
        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiryMinutes = int.Parse(configuration["Jwt:ExpiryMinutes"] ?? "30");

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            ],
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
