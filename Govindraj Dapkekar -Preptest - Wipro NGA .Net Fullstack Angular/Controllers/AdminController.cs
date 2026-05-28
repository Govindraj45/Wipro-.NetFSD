using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrepTestMilestone3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var users = _userManager.Users;
            var userListWithRoles = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userListWithRoles.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName ?? "N/A",
                    Email = user.Email ?? "N/A",
                    Roles = string.Join(", ", roles)
                });
            }

            ViewBag.Message = "Welcome, Admin! You have access to the Admin Dashboard.";
            return View("AdminDashboard", userListWithRoles);
        }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
    }
}
