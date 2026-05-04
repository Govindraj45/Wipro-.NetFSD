using System.ComponentModel.DataAnnotations;

namespace RazorPagesItemsApp.Models;

public class InventoryItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 1000)]
    public int Quantity { get; set; }
}
