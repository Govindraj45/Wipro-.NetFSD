using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBookstoreApp.Filters;

public class AdminInventoryFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated != true)
        {
            context.Result = new RedirectToPageResult("/Account/Login");
            return;
        }

        if (!context.HttpContext.User.IsInRole("Admin"))
        {
            context.Result = new RedirectToPageResult("/Account/AccessDenied");
        }
    }
}
