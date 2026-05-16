namespace OnlineBookstoreApp.Models;

public class OrderSummary
{
    public IReadOnlyList<CartItem> Items { get; set; } = [];
    public decimal Total => Items.Sum(item => item.Price * item.Quantity);
}
