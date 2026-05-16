using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSecurityApp.Models;
using TaskManagementSecurityApp.Services;

namespace TaskManagementSecurityApp.Controllers;

[Authorize(Roles = "User,Admin")]
public class UserController(TaskService tasks) : Controller
{
    public IActionResult TaskList()
    {
        return View(tasks.GetUserTasks(User.Identity?.Name ?? string.Empty));
    }

    [Authorize(Policy = "CanEditTask")]
    [HttpGet]
    public IActionResult EditTask(int id)
    {
        var task = tasks.GetById(id);
        if (task is null)
        {
            return NotFound();
        }

        if (!task.OwnerUserName.Equals(User.Identity?.Name, StringComparison.OrdinalIgnoreCase))
        {
            return Forbid();
        }

        return View(task);
    }

    [Authorize(Policy = "CanEditTask")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditTask(TaskItem model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return tasks.Update(model, User.Identity?.Name ?? string.Empty)
            ? RedirectToAction(nameof(TaskList))
            : Forbid();
    }
}
