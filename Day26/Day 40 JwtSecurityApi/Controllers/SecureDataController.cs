using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtSecurityApi.Controllers;

[ApiController]
[Route("api/secure")]
public class SecureDataController : ControllerBase
{
    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            user = User.Identity?.Name,
            message = "You accessed a JWT-protected endpoint over HTTPS."
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "Admin access granted." });
    }
}
