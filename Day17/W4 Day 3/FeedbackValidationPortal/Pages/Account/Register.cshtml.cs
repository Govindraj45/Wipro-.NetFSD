using FeedbackValidationPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackValidationPortal.Pages.Account;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegistrationInputModel Input { get; set; } = new();

    public IActionResult OnGet() => Page();

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        TempData["RegisteredUser"] = Input.Username;
        return RedirectToPage("/Account/Success");
    }
}
