using AdvancedRazorCatalog.Models;
using AdvancedRazorCatalog.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorCatalog.Pages.Products;

public class DetailsModel(ProductStore store) : PageModel
{
    public Product? Product { get; private set; }

    public void OnGet(int id)
    {
        Product = store.GetById(id);
    }
}
