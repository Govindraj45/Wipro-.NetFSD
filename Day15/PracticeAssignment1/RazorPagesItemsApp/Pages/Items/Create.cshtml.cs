using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesItemsApp.Models;
using RazorPagesItemsApp.Services;

namespace RazorPagesItemsApp.Pages.Items;

public class CreateModel : PageModel
{
    private readonly ItemStore _itemStore;

    public CreateModel(ItemStore itemStore)
    {
        _itemStore = itemStore;
    }

    [BindProperty]
    public InventoryItem Item { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Bound form values are validated before the item is added to the shared store.
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _itemStore.Add(Item);
        return RedirectToPage("/Items/Index");
    }
}
