using System.ComponentModel.DataAnnotations;

namespace AdvancedRazorCatalog.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public List<Category> Categories { get; set; } = [];
}
