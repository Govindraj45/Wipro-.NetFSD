using DatabaseSecurityDemo.Models;
using DatabaseSecurityDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseSecurityDemo.Controllers;

[ApiController]
[Route("api/secure-customers")]
public class SecureCustomersController(CustomerSecurityRepository repository) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(SecureCustomerInput input)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var record = repository.Add(input);
        return Ok(new
        {
            record.Id,
            record.UserName,
            record.Email,
            message = "Sensitive values were hashed, encrypted, and HMAC-protected before storage."
        });
    }

    [HttpGet]
    public IActionResult List()
    {
        return Ok(repository.GetAuditSafeView());
    }
}
