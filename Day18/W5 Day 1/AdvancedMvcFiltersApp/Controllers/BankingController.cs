using AdvancedMvcFiltersApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedMvcFiltersApp.Controllers;

public class BankingController : Controller
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    public IActionResult AccountSummary()
    {
        return Json(new { area = "banking", action = "account-summary" });
    }

    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    public IActionResult Transfer()
    {
        return Json(new { area = "banking", action = "transfer", audit = "transaction logged" });
    }

    [TypeFilter(typeof(RoleAuthorizationFilter), Arguments = new object[] { "Admin" })]
    public IActionResult AdminConsole()
    {
        return Json(new { area = "banking", action = "admin-console", access = "granted" });
    }

    public IActionResult ThrowBankingError()
    {
        throw new InvalidOperationException("Simulated banking exception.");
    }
}
