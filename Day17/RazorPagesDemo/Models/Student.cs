using System.ComponentModel.DataAnnotations;

namespace RazorPagesDemo.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(100, MinimumLength = 3, 
            ErrorMessage = "Name must be between 3 and 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }

        [Required(ErrorMessage = "GPA is required")]
        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0 and 4.0")]
        public decimal GPA { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public string? Course { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
