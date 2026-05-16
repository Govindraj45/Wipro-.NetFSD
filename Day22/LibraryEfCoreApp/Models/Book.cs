using System.ComponentModel.DataAnnotations;

namespace LibraryEfCoreApp.Models;

public class Book
{
    public int BookId { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    public ICollection<Genre> Genres { get; set; } = [];
}
