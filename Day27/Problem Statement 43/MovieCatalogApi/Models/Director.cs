using System.ComponentModel.DataAnnotations;

namespace MovieCatalogApi.Models;

public class Director
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}
