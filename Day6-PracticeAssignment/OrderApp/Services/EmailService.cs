namespace OrderApp.Services;

public class EmailService
{
    public void SendEmail(Order order)
    {
        Console.WriteLine($"Email sent to customer {order.CustomerName} for Order {order.OrderId}");
    }
}
