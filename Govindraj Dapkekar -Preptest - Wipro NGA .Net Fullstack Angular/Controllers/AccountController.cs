using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrepTestMilestone3.Models;
using System.Threading.Tasks;

namespace PrepTestMilestone3.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataProtector _protector;

        public AccountController(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IDataProtectionProvider provider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _protector = provider.CreateProtector("PrepTestMilestone3.AccountController.v1");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            // If already logged in, redirect appropriately
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (User.IsInRole("User"))
                {
                    return RedirectToAction("UserProfile");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else if (roles.Contains("User"))
                        {
                            return RedirectToAction("UserProfile");
                        }
                    }

                    // Fallback redirect
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username and password.");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult UserProfile()
        {
            var username = User.Identity?.Name ?? "User";
            
            // Demonstrating Data Protection API to secure sensitive data
            string sensitiveData = "Phone: +1 (555) 019-2831 | Address: 123 Secure Way, Redmond, WA";
            string encryptedData = _protector.Protect(sensitiveData);
            string decryptedData = _protector.Unprotect(encryptedData);

            ViewBag.Message = $"Welcome, {username}! Here is your profile information.";
            ViewBag.EncryptedSensitiveData = encryptedData;
            ViewBag.DecryptedSensitiveData = decryptedData;

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
