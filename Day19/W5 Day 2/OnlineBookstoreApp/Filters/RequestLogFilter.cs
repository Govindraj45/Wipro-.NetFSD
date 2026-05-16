using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBookstoreApp.Filters;

public class RequestLogFilter(ILogger<RequestLogFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("Handling {Method} {Path}", context.HttpContext.Request.Method, context.HttpContext.Request.Path);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation("Finished with {StatusCode}", context.HttpContext.Response.StatusCode);
    }
}
