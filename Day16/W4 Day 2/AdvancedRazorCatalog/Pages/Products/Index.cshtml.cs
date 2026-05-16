using AdvancedRazorCatalog.Models;
using AdvancedRazorCatalog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorCatalog.Pages.Products;

public class IndexModel(ProductStore store) : PageModel
{
    [BindProperty]
    public ProductInputModel Input { get; set; } = new();

    public IReadOnlyList<Product> Products { get; private set; } = [];

    public void OnGet()
    {
        Products = store.GetAll();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Products = store.GetAll();
            return Page();
        }

        store.Add(Input);
        return RedirectToPage();
    }
}
