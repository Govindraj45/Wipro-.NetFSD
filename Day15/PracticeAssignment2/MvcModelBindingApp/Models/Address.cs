using System.ComponentModel.DataAnnotations;

namespace MvcModelBindingApp.Models;

public class Address
{
    [Required]
    [StringLength(120)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string City { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Zip Code")]
    [StringLength(12)]
    public string ZipCode { get; set; } = string.Empty;
}
