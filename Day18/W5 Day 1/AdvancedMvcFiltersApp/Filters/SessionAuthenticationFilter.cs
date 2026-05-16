using AdvancedMvcFiltersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdvancedMvcFiltersApp.Filters;

public class SessionAuthenticationFilter(IUserContextService users) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!users.IsAuthenticated(context.HttpContext))
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = "Authentication required for this action."
            });
        }
    }
}
