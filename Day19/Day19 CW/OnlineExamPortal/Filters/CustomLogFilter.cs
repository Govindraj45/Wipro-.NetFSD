using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace OnlineExamPortal.Filters
{
    public class CustomLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action Executing");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action Executed");
        }
    }
}
