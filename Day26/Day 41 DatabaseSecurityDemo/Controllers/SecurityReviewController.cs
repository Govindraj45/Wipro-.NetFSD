using DatabaseSecurityDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseSecurityDemo.Controllers;

[ApiController]
[Route("api/security-review")]
public class SecurityReviewController(SecurityAssessmentService assessments) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(assessments.GetAssessmentSummary());
    }
}
