using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBookstoreApp.Filters;

public class SessionCartFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Session.Keys.Contains("BOOKSTORE_CART"))
        {
            context.HttpContext.Session.SetString("BOOKSTORE_CART", "[]");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
