using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingStoreApp.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index(string role)
    {
        var normalizedRole = role.Trim().ToLowerInvariant();

        return normalizedRole switch
        {
            "admin" => Json(new { role = "admin", dashboard = "Administrative insights and product controls." }),
            "customer" => Json(new { role = "customer", dashboard = "Customer recommendations and recent orders." }),
            _ => Json(new { role = normalizedRole, dashboard = "Generic dashboard." })
        };
    }
}
