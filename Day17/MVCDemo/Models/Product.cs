using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required")]
        [Range(0, 10000)]
        public int Stock { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string? Category { get; set; }
    }
}
