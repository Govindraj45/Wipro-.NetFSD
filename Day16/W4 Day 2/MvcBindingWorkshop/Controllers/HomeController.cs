using Microsoft.AspNetCore.Mvc;
using MvcBindingWorkshop.Models;

namespace MvcBindingWorkshop.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new PersonInputModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Submit(PersonInputModel input)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", input);
        }

        return View("Submitted", input);
    }
}
