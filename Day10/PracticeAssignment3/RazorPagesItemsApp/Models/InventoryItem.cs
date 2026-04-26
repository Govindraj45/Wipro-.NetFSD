using System.ComponentModel.DataAnnotations;

namespace RazorPagesItemsApp.Models;

public class InventoryItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 500)]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
