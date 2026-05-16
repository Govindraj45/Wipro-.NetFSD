using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSecurityApp.Services;

namespace TaskManagementSecurityApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(TaskService tasks) : Controller
{
    public IActionResult ManageTasks()
    {
        return View(tasks.GetAllTasks());
    }
}
