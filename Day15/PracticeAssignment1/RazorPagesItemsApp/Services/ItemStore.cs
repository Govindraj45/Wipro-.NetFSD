using RazorPagesItemsApp.Models;

namespace RazorPagesItemsApp.Services;

public class ItemStore
{
    private readonly List<InventoryItem> _items =
    [
        new()
        {
            Id = 1,
            Name = "Laptop",
            Description = "Developer workstation for .NET projects.",
            Quantity = 12
        },
        new()
        {
            Id = 2,
            Name = "Access Card",
            Description = "Office access card issued to new employees.",
            Quantity = 40
        }
    ];

    public IReadOnlyList<InventoryItem> GetAll()
    {
        return _items;
    }

    public void Add(InventoryItem item)
    {
        item.Id = _items.Count == 0 ? 1 : _items.Max(existingItem => existingItem.Id) + 1;
        _items.Add(item);
    }
}
