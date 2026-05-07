using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesDemos.Pages
{
    public class EmployeeFeedbackModel : PageModel
    {
        // One-Way Binding: We just read this to display on the UI
        public string EmployeeName { get; private set; }

        // Two-Way Binding: We bind this to the form input and use validation
        [BindProperty]
        [Required(ErrorMessage = "Feedback cannot be empty.")]
        [MinLength(10, ErrorMessage = "Feedback must be at least 10 characters long.")]
        public string Feedback { get; set; }

        public string ConfirmationMessage { get; set; }

        public void OnGet()
        {
            // Simulate retrieving employee data from a database
            EmployeeName = "Jane Smith (Engineering)";
        }

        public void OnPost()
        {
            // The EmployeeName isn't posted back because it's only one-way bound, so we must reload it.
            EmployeeName = "Jane Smith (Engineering)";

            if (ModelState.IsValid)
            {
                ConfirmationMessage = "Thank you for your feedback, " + EmployeeName.Split(' ')[0] + "! It has been securely submitted.";
            }
        }
    }
}
