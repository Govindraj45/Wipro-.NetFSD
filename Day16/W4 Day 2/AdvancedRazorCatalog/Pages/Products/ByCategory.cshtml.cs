using AdvancedRazorCatalog.Models;
using AdvancedRazorCatalog.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorCatalog.Pages.Products;

public class ByCategoryModel(ProductStore store) : PageModel
{
    public string Slug { get; private set; } = string.Empty;
    public IReadOnlyList<Product> Products { get; private set; } = [];

    public void OnGet(string slug)
    {
        Slug = slug;
        Products = store.GetByCategory(slug);
    }
}
