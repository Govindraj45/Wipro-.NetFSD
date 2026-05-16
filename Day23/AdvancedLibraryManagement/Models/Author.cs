using System.ComponentModel.DataAnnotations;

namespace AdvancedLibraryManagement.Models;

public class Author
{
    public int AuthorId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = [];
}
