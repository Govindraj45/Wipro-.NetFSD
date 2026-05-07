using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OnlineBankingApp.Filters
{
    public class TransactionLoggingFilter : IActionFilter
    {
        private readonly ILogger<TransactionLoggingFilter> _logger;

        public TransactionLoggingFilter(ILogger<TransactionLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Request.Headers["X-User-Id"].ToString() ?? "Unknown";
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"[Transaction] User '{userId}' is initiating action '{actionName}' at {System.DateTime.UtcNow}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var userId = context.HttpContext.Request.Headers["X-User-Id"].ToString() ?? "Unknown";
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"[Transaction] User '{userId}' completed action '{actionName}' at {System.DateTime.UtcNow}");
        }
    }
}
