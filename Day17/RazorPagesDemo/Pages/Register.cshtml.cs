using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models;

namespace RazorPagesDemo.Pages
{
    public class RegisterModel : PageModel
    {
        // Property Binding - GET Binding: Gets data from URL query string
        [BindProperty(SupportsGet = true)]
        public string? SearchName { get; set; }

        // Property Binding - POST Binding: Gets data from form submission
        [BindProperty]
        public Student? Student { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

        // OnGet() - Handles GET request
        // Lifecycle Stage 1
        public void OnGet()
        {
            // Simulate data from database
            Students = new List<Student>
            {
                new Student { StudentId = 1, Name = "Raj Kumar", Email = "raj@gmail.com", Age = 20, GPA = 3.8m, Course = "CS", EnrollmentDate = DateTime.Now },
                new Student { StudentId = 2, Name = "Priya Singh", Email = "priya@gmail.com", Age = 21, GPA = 3.9m, Course = "IT", EnrollmentDate = DateTime.Now },
                new Student { StudentId = 3, Name = "Amit Patel", Email = "amit@gmail.com", Age = 22, GPA = 3.6m, Course = "ECE", EnrollmentDate = DateTime.Now }
            };

            // Filter students by search name if provided
            if (!string.IsNullOrEmpty(SearchName))
            {
                Students = Students.Where(s => s.Name!.Contains(SearchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        // OnPost() - Handles POST request (form submission)
        // Lifecycle Stage 2
        public IActionResult OnPost()
        {
            // Validation Execution - Stage 5
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add new student (simulated)
            if (Student != null)
            {
                Student.StudentId = new Random().Next(1000, 9999);
                Students.Add(Student);
                return RedirectToPage("Success", new { id = Student.StudentId });
            }

            return Page();
        }
    }
}
