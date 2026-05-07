using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesDemos.Pages
{
    public class StudentRegistrationModel : PageModel
    {
        [BindProperty]
        public string StudentName { get; set; }
        
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!string.IsNullOrEmpty(StudentName))
            {
                Message = "Welcome " + StudentName + " to the portal!";
            }
            else
            {
                Message = "Please enter a valid name.";
            }
        }
    }
}
