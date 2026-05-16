using System.ComponentModel.DataAnnotations;

namespace SecureShoppingPlatform.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 100000)]
    public decimal Price { get; set; }

    public List<string> Reviews { get; set; } = [];
}
