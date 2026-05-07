using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ECommerceApp.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;
        private Stopwatch _stopwatch;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
            var request = context.HttpContext.Request;
            _logger.LogInformation($"[Request] {request.Method} {request.Path}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var response = context.HttpContext.Response;
            _logger.LogInformation($"[Response] Status: {response.StatusCode} completed in {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
