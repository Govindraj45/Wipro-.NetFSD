using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSecurityApp.Models;
using TaskManagementSecurityApp.Services;

namespace TaskManagementSecurityApp.Controllers;

public class AccountController(UserDirectoryService users) : Controller
{
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

        var user = users.Validate(model.UserName, model.Password);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, user.Role),
            new("CanEditTask", user.CanEditTask ? "true" : "false")
        };

        await HttpContext.SignInAsync("TaskCookie", new ClaimsPrincipal(new ClaimsIdentity(claims, "TaskCookie")));

        return user.Role == "Admin"
            ? RedirectToAction("ManageTasks", "Admin")
            : RedirectToAction("TaskList", "User");
    }

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("TaskCookie");
        return RedirectToAction(nameof(Login));
    }

    public IActionResult AccessDenied() => View();
}
