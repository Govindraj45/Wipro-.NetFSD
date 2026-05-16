using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecureShoppingPlatform.Models;
using SecureShoppingPlatform.Services;

namespace SecureShoppingPlatform.Controllers;

public class AccountController(UserStoreService users) : Controller
{
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (users.Exists(model.UserName))
        {
            ModelState.AddModelError(string.Empty, "Username already exists.");
            return View(model);
        }

        users.Register(model);
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var (user, error) = users.Validate(model.UserName, model.Password);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, error ?? "Invalid login.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SecureShopCookie"));
        await HttpContext.SignInAsync("SecureShopCookie", principal);
        return RedirectToAction("Index", "Products");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("SecureShopCookie");
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}
