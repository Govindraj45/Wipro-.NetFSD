using AdvancedMvcFiltersApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedMvcFiltersApp.Controllers;

public class StoreController : Controller
{
    public IActionResult Index()
    {
        return Json(new
        {
            app = "AdvancedMvcFiltersApp",
            scenarios = new[]
            {
                "e-commerce logging and auth filters",
                "online banking auth, role auth, logging, and error filters"
            }
        });
    }

    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    public IActionResult Checkout()
    {
        return Json(new { area = "store", action = "checkout", status = "authenticated" });
    }

    public IActionResult ThrowStoreError()
    {
        throw new InvalidOperationException("Simulated store exception.");
    }
}
