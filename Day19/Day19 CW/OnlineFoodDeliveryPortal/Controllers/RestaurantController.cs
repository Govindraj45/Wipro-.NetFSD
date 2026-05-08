using Microsoft.AspNetCore.Mvc;

namespace OnlineFoodDeliveryPortal.Controllers
{
    [Route("restaurant")]
    public class RestaurantController : Controller
    {
        [Route("menu")]
        public IActionResult Menu()
        {
            return Content("Restaurant Menu Page");
        }

        [Route("details/{id:int}")] // Attribute Routing with Constraint
        public IActionResult Details(int id)
        {
            return Content("Restaurant Details ID: " + id);
        }
    }
}
