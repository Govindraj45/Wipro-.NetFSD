using System.ComponentModel.DataAnnotations;

namespace BookstoreAdoNetApp.Models;

public class Book
{
    public int BookId { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [Range(0.01, 99999)]
    public decimal Price { get; set; }

    [Range(1000, 9999)]
    public int PublishedYear { get; set; }
}
