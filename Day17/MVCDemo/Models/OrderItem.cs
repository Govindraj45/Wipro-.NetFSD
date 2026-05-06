using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models
{
    // Model-level validation using IValidatableObject
    public class OrderItem : IValidatableObject
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Unit price must be positive")]
        public decimal UnitPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Discount cannot exceed 100%")]
        public decimal DiscountPercent { get; set; } = 0;

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Range(typeof(DateTime), "2024-01-01", "2030-12-31", 
            ErrorMessage = "Order date must be between 2024 and 2030")]
        public DateTime DeliveryDate { get; set; }

        // Model-level validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Business rule: Delivery date must be after order date
            if (DeliveryDate <= OrderDate)
            {
                yield return new ValidationResult(
                    "Delivery date must be after order date",
                    new[] { nameof(DeliveryDate) });
            }

            // Business rule: Total amount cannot exceed 1,000,000
            decimal totalAmount = Quantity * UnitPrice * (1 - DiscountPercent / 100);
            if (totalAmount > 1000000)
            {
                yield return new ValidationResult(
                    "Total order amount cannot exceed 1,000,000",
                    new[] { nameof(Quantity), nameof(UnitPrice), nameof(DiscountPercent) });
            }

            // Business rule: Bulk orders get automatic discount
            if (Quantity > 100 && DiscountPercent == 0)
            {
                yield return new ValidationResult(
                    "Orders of 100+ units should have a discount applied",
                    new[] { nameof(DiscountPercent) });
            }
        }
    }
}
