using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingStoreApp.Controllers;

public class ProductsController : Controller
{
    public IActionResult Details(string category, int id)
    {
        return Json(new
        {
            route = "product-details",
            category,
            productId = id
        });
    }

    public IActionResult Filter(string category, string priceRange)
    {
        return Json(new
        {
            route = "product-filter",
            category,
            priceRange
        });
    }

    public IActionResult Checkout(string state = "guest")
    {
        if (state.Equals("guest", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction("LoginRequired", "Users");
        }

        return RedirectToAction("Summary", new { state });
    }

    public IActionResult Summary(string state)
    {
        return Json(new
        {
            route = "checkout-summary",
            userState = state,
            message = "Dynamic routing allowed checkout based on user state."
        });
    }
}
