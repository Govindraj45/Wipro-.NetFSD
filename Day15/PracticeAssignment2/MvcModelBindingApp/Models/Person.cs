using System.ComponentModel.DataAnnotations;

namespace MvcModelBindingApp.Models;

public class Person
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(60)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(60)]
    public string LastName { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Age { get; set; }

    public Address Address { get; set; } = new();
}
