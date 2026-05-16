namespace AdvancedMvcFiltersApp.Services;

public interface IUserContextService
{
    bool IsAuthenticated(HttpContext context);
    string GetRole(HttpContext context);
    string GetUserId(HttpContext context);
}
