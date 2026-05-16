using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int AuthorId { get; set; }

    [Range(1000, 9999)]
    public int PublicationYear { get; set; }
}
