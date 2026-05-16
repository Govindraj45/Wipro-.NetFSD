using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingStoreApp.Controllers;

public class UsersController : Controller
{
    public IActionResult Orders(string username, int? orderId)
    {
        return Json(new
        {
            route = "user-orders",
            username,
            orderId = orderId ?? 0
        });
    }

    public IActionResult LoginRequired()
    {
        return Json(new
        {
            route = "login-required",
            message = "Guests are redirected here before checkout."
        });
    }
}
