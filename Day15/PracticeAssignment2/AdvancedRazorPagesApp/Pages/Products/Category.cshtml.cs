using AdvancedRazorPagesApp.Models;
using AdvancedRazorPagesApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPagesApp.Pages.Products;

public class CategoryModel : PageModel
{
    private readonly ProductStore _productStore;

    public CategoryModel(ProductStore productStore)
    {
        _productStore = productStore;
    }

    public string CategoryName { get; private set; } = string.Empty;

    public IReadOnlyList<Product> Products { get; private set; } = [];

    public void OnGet(string categoryName)
    {
        CategoryName = categoryName;
        Products = _productStore.GetByCategory(categoryName);
    }
}
