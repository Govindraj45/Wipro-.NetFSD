using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesItemsApp.Models;
using RazorPagesItemsApp.Services;

namespace RazorPagesItemsApp.Pages.Items;

public class IndexModel : PageModel
{
    private readonly ItemStore _itemStore;

    public IndexModel(ItemStore itemStore)
    {
        _itemStore = itemStore;
    }

    public IReadOnlyList<InventoryItem> Items { get; private set; } = [];

    public void OnGet()
    {
        Items = _itemStore.GetAll();
    }
}
