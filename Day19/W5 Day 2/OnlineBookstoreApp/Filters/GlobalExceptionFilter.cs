using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBookstoreApp.Filters;

public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Unhandled exception in OnlineBookstoreApp.");
        context.Result = new ObjectResult(new { message = "Something went wrong while processing the request." })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}
