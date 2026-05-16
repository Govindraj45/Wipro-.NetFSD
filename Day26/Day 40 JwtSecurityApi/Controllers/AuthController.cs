using JwtSecurityApi.Models;
using JwtSecurityApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtSecurityApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserAuthService users, TokenService tokens) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var user = users.Validate(request);
        if (user is null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        return Ok(new
        {
            token = tokens.CreateToken(user),
            role = user.Role
        });
    }
}
