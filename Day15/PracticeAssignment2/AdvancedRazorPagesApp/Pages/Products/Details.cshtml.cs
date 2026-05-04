using AdvancedRazorPagesApp.Models;
using AdvancedRazorPagesApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPagesApp.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly ProductStore _productStore;

    public DetailsModel(ProductStore productStore)
    {
        _productStore = productStore;
    }

    public Product? Product { get; private set; }

    public void OnGet(int id)
    {
        Product = _productStore.GetById(id);
    }
}
