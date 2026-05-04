using AdvancedRazorPagesApp.Models;
using AdvancedRazorPagesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPagesApp.Pages.Products;

public class IndexModel : PageModel
{
    private readonly ProductStore _productStore;

    public IndexModel(ProductStore productStore)
    {
        _productStore = productStore;
    }

    public IReadOnlyList<Product> Products { get; private set; } = [];

    [BindProperty]
    public Product Product { get; set; } = CreateBlankProduct();

    public void OnGet()
    {
        LoadProducts();
    }

    public IActionResult OnPost()
    {
        if (_productStore.ProductIdExists(Product.ProductID))
        {
            ModelState.AddModelError("Product.ProductID", "Product ID already exists.");
        }

        if (!Product.Categories.Any(category => !string.IsNullOrWhiteSpace(category.Name)))
        {
            ModelState.AddModelError("Product.Categories[0].Name", "Enter at least one category.");
        }

        if (!ModelState.IsValid)
        {
            LoadProducts();
            EnsureCategoryInputs();
            return Page();
        }

        _productStore.Add(Product);
        return RedirectToPage();
    }

    private static Product CreateBlankProduct()
    {
        return new Product
        {
            Categories =
            [
                new Category(),
                new Category(),
                new Category()
            ]
        };
    }

    private void EnsureCategoryInputs()
    {
        while (Product.Categories.Count < 3)
        {
            Product.Categories.Add(new Category());
        }
    }

    private void LoadProducts()
    {
        Products = _productStore.GetAll();
    }
}
