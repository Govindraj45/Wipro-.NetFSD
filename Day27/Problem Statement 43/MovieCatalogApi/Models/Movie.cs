using System.ComponentModel.DataAnnotations;

namespace MovieCatalogApi.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DirectorId { get; set; }

    [Range(1888, 9999)]
    public int ReleaseYear { get; set; }
}
