using System.ComponentModel.DataAnnotations;

namespace LibraryEfCoreApp.Models;

public class Genre
{
    public int GenreId { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = [];
}
