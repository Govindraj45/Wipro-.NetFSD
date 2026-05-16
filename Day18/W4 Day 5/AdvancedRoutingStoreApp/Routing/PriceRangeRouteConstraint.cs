using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AdvancedRoutingStoreApp.Routing;

public class PriceRangeRouteConstraint : IRouteConstraint
{
    private static readonly HashSet<string> AllowedRanges = new(StringComparer.OrdinalIgnoreCase)
    {
        "budget",
        "standard",
        "premium"
    };

    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return values.TryGetValue(routeKey, out var value)
               && value is not null
               && AllowedRanges.Contains(value.ToString() ?? string.Empty);
    }
}
