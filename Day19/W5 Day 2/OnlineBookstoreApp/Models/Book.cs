using System.ComponentModel.DataAnnotations;
using OnlineBookstoreApp.Validation;

namespace OnlineBookstoreApp.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [Required]
    [Isbn]
    public string Isbn { get; set; } = string.Empty;

    [Range(1, 10000)]
    public decimal Price { get; set; }

    [StringLength(250)]
    public string Description { get; set; } = string.Empty;
}
