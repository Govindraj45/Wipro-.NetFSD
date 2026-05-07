using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Filters;

namespace OnlineBankingApp.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return Content("Account Management");
        }

        [ServiceFilter(typeof(TransactionLoggingFilter))]
        public IActionResult Transfer()
        {
            return Content("Transfer Successful!");
        }

        [TypeFilter(typeof(RoleAuthorizationFilter), Arguments = new object[] { "Admin" })]
        public IActionResult AdminDashboard()
        {
            return Content("Welcome Admin!");
        }
    }
}
