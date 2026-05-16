using Microsoft.AspNetCore.Mvc.Filters;
using AdvancedMvcFiltersApp.Services;

namespace AdvancedMvcFiltersApp.Filters;

public class AuditLoggingFilter(ILogger<AuditLoggingFilter> logger, IUserContextService users) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation(
            "Request {Method} {Path} by {UserId}",
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            users.GetUserId(context.HttpContext));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation(
            "Response status {StatusCode} for {Path}",
            context.HttpContext.Response.StatusCode,
            context.HttpContext.Request.Path);
    }
}
