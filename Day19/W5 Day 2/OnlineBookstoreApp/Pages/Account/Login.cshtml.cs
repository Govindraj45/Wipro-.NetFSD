using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstoreApp.Models;
using OnlineBookstoreApp.Services;

namespace OnlineBookstoreApp.Pages.Account;

public class LoginModel(UserService users) : PageModel
{
    [BindProperty]
    public LoginInputModel Input { get; set; } = new();

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = users.ValidateCredentials(Input.UserName, Input.Password);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, "BookstoreCookie");
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync("BookstoreCookie", principal);

        return RedirectToAction("Index", "Books");
    }
}
