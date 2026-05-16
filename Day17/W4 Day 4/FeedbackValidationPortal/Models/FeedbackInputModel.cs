using System.ComponentModel.DataAnnotations;

namespace FeedbackValidationPortal.Models;

public class FeedbackInputModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(250)]
    public string Comments { get; set; } = string.Empty;
}
