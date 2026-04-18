using System.Collections.Generic;

class Order
{
    public Order(int orderId, int customerId, string productName, string category, decimal totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        ProductName = productName;
        Category = category;
        TotalAmount = totalAmount;
        StatusHistory = new Stack<string>();
    }

    public int OrderId { get; }
    public int CustomerId { get; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public decimal TotalAmount { get; set; }
    public Stack<string> StatusHistory { get; }
}
