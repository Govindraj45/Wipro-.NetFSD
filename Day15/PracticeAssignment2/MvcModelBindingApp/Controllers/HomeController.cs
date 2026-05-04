using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcModelBindingApp.Models;

namespace MvcModelBindingApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new Person());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Person person)
    {
        // MVC model binding maps simple properties and nested Address fields from the posted form.
        if (!ModelState.IsValid)
        {
            return View(person);
        }

        return View("Submitted", person);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
