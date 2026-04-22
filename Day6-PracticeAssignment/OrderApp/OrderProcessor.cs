namespace OrderApp;

public class OrderProcessor
{
    public event OrderPlacedHandler? OnOrderPlaced;

    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Order Placed: {order.OrderId}");
        Console.WriteLine($"Customer: {order.CustomerName}");
        Console.WriteLine($"Amount: Rs. {order.Amount}");

        NotifySubscribers(order);
    }

    public void ProcessOrderWithAction(Order order, Action<Order> notificationAction)
    {
        Console.WriteLine($"Order Placed: {order.OrderId}");
        notificationAction(order);
    }

    private void NotifySubscribers(Order order)
    {
        if (OnOrderPlaced is null)
        {
            Console.WriteLine("No notification subscribers found.");
            return;
        }

        foreach (OrderPlacedHandler handler in OnOrderPlaced.GetInvocationList())
        {
            try
            {
                handler(order);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Notification failed: {exception.Message}");
            }
        }
    }
}
