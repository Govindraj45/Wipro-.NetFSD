using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models;

public class Author
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}
