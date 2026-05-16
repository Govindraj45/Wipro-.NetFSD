namespace AdvancedMvcFiltersApp.Services;

public class DemoUserContextService : IUserContextService
{
    public bool IsAuthenticated(HttpContext context)
    {
        return context.Request.Headers.TryGetValue("X-User-Auth", out var auth)
               && auth.ToString().Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    public string GetRole(HttpContext context)
    {
        return context.Request.Headers.TryGetValue("X-User-Role", out var role)
            ? role.ToString()
            : "Guest";
    }

    public string GetUserId(HttpContext context)
    {
        return context.Request.Headers.TryGetValue("X-User-Id", out var userId)
            ? userId.ToString()
            : "anonymous";
    }
}
