using System.ComponentModel.DataAnnotations;

namespace AdvancedRazorCatalog.Models;

public class ProductInputModel
{
    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public List<CategoryInputModel> Categories { get; set; } =
    [
        new(),
        new()
    ];
}
