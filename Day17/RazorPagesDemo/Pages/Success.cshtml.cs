using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesDemo.Pages
{
    public class SuccessModel : PageModel
    {
        public int StudentId { get; set; }

        public void OnGet(int id)
        {
            StudentId = id;
        }
    }
}
