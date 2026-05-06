using CourseRegistrationPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistrationPortal.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // In a real application, you would save this to a database
                TempData["SuccessMessage"] = $"Successfully registered {student.Name} for {student.CourseName}!";
                return RedirectToAction("Create");
            }

            return View(student);
        }
    }
}
