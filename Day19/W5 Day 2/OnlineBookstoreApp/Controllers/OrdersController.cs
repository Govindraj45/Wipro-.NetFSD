using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreApp.Services;

namespace OnlineBookstoreApp.Controllers;

[Authorize]
public class OrdersController(OrderService orders) : Controller
{
    public IActionResult Summary()
    {
        return View(orders.GetSummary());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirm()
    {
        var summary = orders.ConfirmOrder();
        return View("Confirmation", summary);
    }
}
