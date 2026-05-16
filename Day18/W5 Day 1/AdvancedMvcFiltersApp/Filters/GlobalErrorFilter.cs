using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdvancedMvcFiltersApp.Filters;

public class GlobalErrorFilter(ILogger<GlobalErrorFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Unhandled exception while processing MVC request.");
        context.Result = new ObjectResult(new
        {
            message = "A user-friendly error message was returned by the global filter."
        })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}
