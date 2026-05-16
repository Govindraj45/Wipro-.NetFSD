using System.ComponentModel.DataAnnotations;

namespace MvcBindingWorkshop.Models;

public class PersonInputModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Age { get; set; }

    public Address Address { get; set; } = new();
}
