namespace Day4PracticeAssignmentApp.Scenarios.ECommerce;

class Order
{
    public Order(int orderId, int customerId, string productName, double price, string category)
    {
        OrderId = orderId;
        CustomerId = customerId;
        ProductName = productName;
        Price = price;
        Category = category;
        StatusHistory = new Stack<string>();
    }

    public int OrderId { get; }
    public int CustomerId { get; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public Stack<string> StatusHistory { get; }
}
