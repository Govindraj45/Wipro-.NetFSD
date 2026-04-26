using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesItemsApp.Models;

namespace RazorPagesItemsApp.Pages.Items;

public class IndexModel : PageModel
{
    private readonly ItemStore _itemStore;

    public IndexModel(ItemStore itemStore)
    {
        _itemStore = itemStore;
    }

    public IReadOnlyList<InventoryItem> Items { get; private set; } = [];

    [TempData]
    public string? Message { get; set; }

    public void OnGet()
    {
        // PageModel prepares the dynamic data consumed by the Razor foreach loop.
        Items = _itemStore.GetAll();
    }
}
