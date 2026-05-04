using System.ComponentModel.DataAnnotations;

namespace AdvancedRazorPagesApp.Models;

public class Product
{
    [Range(1, int.MaxValue, ErrorMessage = "Product ID must be greater than zero.")]
    public int ProductID { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    public List<Category> Categories { get; set; } = [];
}
