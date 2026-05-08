using Microsoft.AspNetCore.Mvc;

namespace OnlineExamPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string username = "Student1")
        {
            // Set Session
            HttpContext.Session.SetString("User", username);

            // Set Cookie
            Response.Cookies.Append("Theme", "Dark");

            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            // Get Session
            var user = HttpContext.Session.GetString("User");
            
            // Get Cookie
            var theme = Request.Cookies["Theme"] ?? "Light";

            return Content("Welcome " + user + ". Your current theme is: " + theme);
        }
    }
}
