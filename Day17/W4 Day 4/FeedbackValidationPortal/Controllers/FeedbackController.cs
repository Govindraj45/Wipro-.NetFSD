using FeedbackValidationPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedbackValidationPortal.Controllers;

public class FeedbackController : Controller
{
    private static readonly List<FeedbackInputModel> FeedbackItems = [];

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Submitted = FeedbackItems;
        ViewBag.Ratings = new List<SelectListItem>
        {
            new("1 Star", "1"),
            new("2 Stars", "2"),
            new("3 Stars", "3"),
            new("4 Stars", "4"),
            new("5 Stars", "5")
        };

        return View(new FeedbackInputModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Submit(FeedbackInputModel input)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Submitted = FeedbackItems;
            ViewBag.Ratings = new List<SelectListItem>
            {
                new("1 Star", "1"),
                new("2 Stars", "2"),
                new("3 Stars", "3"),
                new("4 Stars", "4"),
                new("5 Stars", "5")
            };
            return View("Index", input);
        }

        FeedbackItems.Add(input);
        return RedirectToAction(nameof(Submissions));
    }

    public IActionResult Submissions()
    {
        return View(FeedbackItems);
    }
}
