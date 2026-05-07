using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBankingApp.Filters
{
    public class AuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Simulate authentication check.
            var isAuthenticated = context.HttpContext.Request.Headers.ContainsKey("X-User-Auth") || 
                                  context.HttpContext.User.Identity?.IsAuthenticated == true;
                                  
            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
