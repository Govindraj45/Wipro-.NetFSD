using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBankingApp.Filters
{
    public class RoleAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public RoleAuthorizationFilter(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Request.Headers["X-User-Role"].ToString();
            
            // If the user doesn't have the required role, return Forbidden
            if (string.IsNullOrEmpty(userRole) || userRole != _requiredRole)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
