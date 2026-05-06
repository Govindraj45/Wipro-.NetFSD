using System.ComponentModel.DataAnnotations;

namespace RoutingMasterApp.Models
{
    public class BlogPost
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 5)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(200)]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug must contain only lowercase letters, numbers, and hyphens")]
        public string? Slug { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(5000, MinimumLength = 50)]
        public string? Content { get; set; }

        [Required]
        [StringLength(50)]
        public string? Category { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be 1-5")]
        public int Rating { get; set; } = 5;

        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [Display(Name = "View Count")]
        public int ViewCount { get; set; } = 0;

        public string? AuthorName { get; set; }
    }
}
