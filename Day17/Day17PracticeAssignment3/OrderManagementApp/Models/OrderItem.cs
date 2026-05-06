using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementApp.Models
{
    public class OrderItem : IValidatableObject
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10000")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be positive")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage cannot exceed 100%")]
        [Display(Name = "Discount %")]
        public decimal DiscountPercent { get; set; } = 0;

        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Item Total")]
        public decimal ItemTotal => Quantity * UnitPrice * (1 - DiscountPercent / 100);

        // Navigation property
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        // Model-level validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Rule 1: Item total cannot exceed 5 lakhs per item
            var itemTotal = Quantity * UnitPrice * (1 - DiscountPercent / 100);
            if (itemTotal > 500000)
            {
                yield return new ValidationResult(
                    "Single item total cannot exceed ₹5,00,000",
                    new[] { nameof(Quantity), nameof(UnitPrice) });
            }

            // Rule 2: Bulk orders must have discount
            if (Quantity > 500 && DiscountPercent == 0)
            {
                yield return new ValidationResult(
                    "Bulk orders (500+ units) must have a discount applied",
                    new[] { nameof(DiscountPercent) });
            }

            // Rule 3: Minimum discount for bulk
            if (Quantity > 500 && DiscountPercent < 5)
            {
                yield return new ValidationResult(
                    "Bulk orders must have at least 5% discount",
                    new[] { nameof(DiscountPercent) });
            }
        }
    }
}
