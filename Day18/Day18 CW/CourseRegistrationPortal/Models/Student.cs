using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationPortal.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select a course.")]
        public string SelectedCourse { get; set; }
    }
}
