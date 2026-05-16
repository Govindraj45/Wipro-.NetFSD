using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingStoreApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Json(new
        {
            app = "AdvancedRoutingStoreApp",
            routes = new[]
            {
                "/products/books/101",
                "/products/filter/electronics/premium",
                "/users/govind/orders/42",
                "/promo/{guid}",
                "/dashboard/admin"
            }
        });
    }

    public IActionResult Promo(Guid promoId)
    {
        return Json(new
        {
            message = "Promo route accepted a valid GUID.",
            promoId
        });
    }
}
