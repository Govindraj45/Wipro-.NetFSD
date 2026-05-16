using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstoreApp.Models;
using OnlineBookstoreApp.Services;

namespace OnlineBookstoreApp.Pages.Account;

public class RegisterModel(UserService users) : PageModel
{
    [BindProperty]
    public RegisterInputModel Input { get; set; } = new();

    public IActionResult OnGet() => Page();

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (users.UserNameExists(Input.UserName))
        {
            ModelState.AddModelError(string.Empty, "Username already exists.");
            return Page();
        }

        users.Register(Input);
        return RedirectToPage("/Account/Login");
    }
}
