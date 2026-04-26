using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesItemsApp.Models;

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

    [TempData]
    public string? Message { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // ModelState applies the validation attributes from InventoryItem before saving.
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _itemStore.Add(Item);
        Message = $"{Item.Name} was added successfully.";

        return RedirectToPage("/Items/Index");
    }
}
