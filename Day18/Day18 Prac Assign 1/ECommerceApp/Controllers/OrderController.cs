using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Filters;

namespace ECommerceApp.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    public class OrderController : Controller
    {
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult Checkout()
        {
            return Content("Checkout successful!");
        }

        public IActionResult ErrorTest()
        {
            throw new System.Exception("This is a simulated exception to test the global error filter.");
        }
    }
}
