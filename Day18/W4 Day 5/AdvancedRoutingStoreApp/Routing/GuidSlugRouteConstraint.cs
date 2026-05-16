using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AdvancedRoutingStoreApp.Routing;

public class GuidSlugRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return values.TryGetValue(routeKey, out var value)
               && value is not null
               && Guid.TryParse(value.ToString(), out _);
    }
}
