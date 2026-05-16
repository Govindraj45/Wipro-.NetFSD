using System.ComponentModel.DataAnnotations;

namespace SecureShoppingPlatform.Models;

public class ReviewInputModel
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(200)]
    public string Comment { get; set; } = string.Empty;
}
