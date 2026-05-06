using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementApp.Models
{
    public class Order : IValidatableObject
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Delivery Date is required")]
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20)]
        [Display(Name = "Order Status")]
        public string? Status { get; set; } = "Pending";

        [Display(Name = "Shipping Address")]
        [StringLength(200)]
        public string? ShippingAddress { get; set; }

        [Display(Name = "Special Instructions")]
        [StringLength(500)]
        public string? SpecialInstructions { get; set; }

        [Range(0, 1000000, ErrorMessage = "Discount cannot exceed total amount")]
        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; } = 0;

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; } = 0;

        // Navigation property
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public ICollection<OrderItem>? Items { get; set; }

        // Model-level validation using IValidatableObject
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Rule 1: Delivery date must be after order date
            if (DeliveryDate <= OrderDate)
            {
                yield return new ValidationResult(
                    "Delivery date must be after order date",
                    new[] { nameof(DeliveryDate) });
            }

            // Rule 2: Delivery date must be within 30 days
            if ((DeliveryDate - OrderDate).TotalDays > 30)
            {
                yield return new ValidationResult(
                    "Delivery date cannot be more than 30 days from order date",
                    new[] { nameof(DeliveryDate) });
            }

            // Rule 3: Status must be valid
            var validStatuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
            if (!validStatuses.Contains(Status))
            {
                yield return new ValidationResult(
                    $"Status must be one of: {string.Join(", ", validStatuses)}",
                    new[] { nameof(Status) });
            }

            // Rule 4: Discount cannot exceed total
            if (Items != null && Items.Any())
            {
                var itemTotal = Items.Sum(i => i.Quantity * i.UnitPrice);
                if (DiscountAmount > itemTotal)
                {
                    yield return new ValidationResult(
                        "Discount amount cannot exceed total order amount",
                        new[] { nameof(DiscountAmount) });
                }

                // Rule 5: Total amount limit check
                if (itemTotal - DiscountAmount > 10000000)  // 1 crore limit
                {
                    yield return new ValidationResult(
                        "Order total exceeds maximum allowed limit of ₹1,00,00,000");
                }
            }

            // Rule 6: Shipping address required for delivery
            if (Status == "Shipped" || Status == "Delivered")
            {
                if (string.IsNullOrWhiteSpace(ShippingAddress))
                {
                    yield return new ValidationResult(
                        "Shipping address is required for shipped orders",
                        new[] { nameof(ShippingAddress) });
                }
            }
        }
    }
}
