using RazorPagesItemsApp.Models;

namespace RazorPagesItemsApp;

/// <summary>
/// Keeps sample item data in memory so Razor Pages can display updates after form posts.
/// </summary>
public class ItemStore
{
    private readonly List<InventoryItem> _items =
    [
        new()
        {
            Id = 1,
            Name = "Laptop",
            Description = "Training lab laptop assigned to a learner.",
            Quantity = 12,
            CreatedAt = DateTime.Today.AddDays(-2)
        },
        new()
        {
            Id = 2,
            Name = "Headset",
            Description = "USB headset for virtual cohort sessions.",
            Quantity = 20,
            CreatedAt = DateTime.Today.AddDays(-1)
        }
    ];

    public IReadOnlyList<InventoryItem> GetAll()
    {
        return _items
            .OrderBy(item => item.Name)
            .ToList()
            .AsReadOnly();
    }

    public void Add(InventoryItem item)
    {
        item.Id = _items.Count == 0 ? 1 : _items.Max(existingItem => existingItem.Id) + 1;
        item.CreatedAt = DateTime.Now;

        _items.Add(item);
    }
}
