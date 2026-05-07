using Microsoft.AspNetCore.Mvc;
using CourseRegistrationPortal.Models;

namespace CourseRegistrationPortal.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                // Server-Side Validation Passed
                ViewBag.Message = $"Registration successful for {student.Name} in course {student.SelectedCourse}!";
                return View("Success");
            }

            // Server-Side Validation Failed (Return to form with errors)
            return View(student);
        }
    }
}
