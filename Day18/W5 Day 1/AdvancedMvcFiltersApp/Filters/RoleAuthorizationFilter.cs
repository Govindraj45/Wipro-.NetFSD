using AdvancedMvcFiltersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdvancedMvcFiltersApp.Filters;

public class RoleAuthorizationFilter(IUserContextService users, string requiredRole) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var role = users.GetRole(context.HttpContext);
        if (!role.Equals(requiredRole, StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new ForbidResult();
        }
    }
}
