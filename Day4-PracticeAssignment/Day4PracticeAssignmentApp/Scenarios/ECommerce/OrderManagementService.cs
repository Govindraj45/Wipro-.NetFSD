namespace Day4PracticeAssignmentApp.Scenarios.ECommerce;

class OrderManagementService : IOrderManagementService
{
    private readonly List<Order> orders = new();
    private readonly Dictionary<int, Customer> customers = new();
    private readonly HashSet<string> categories = new(StringComparer.OrdinalIgnoreCase);
    private Queue<Order> processingQueue = new();

    public bool HasOrdersToProcess => processingQueue.Count > 0;

    public void AddCustomer(Customer customer)
    {
        customers[customer.CustomerId] = customer;
    }

    public void AddOrder(Order order)
    {
        if (!customers.ContainsKey(order.CustomerId))
        {
            Console.WriteLine($"Customer {order.CustomerId} does not exist. Order was not added.");
            return;
        }

        order.StatusHistory.Push("Placed");
        orders.Add(order);
        categories.Add(order.Category);
        processingQueue.Enqueue(order);
    }

    public void UpdateOrder(int orderId, string productName, double price, string category)
    {
        Order? order = FindOrder(orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        order.ProductName = productName;
        order.Price = price;
        order.Category = category;
        UpdateStatus(orderId, "Updated");
        RefreshCategories();
    }

    public void RemoveOrder(int orderId)
    {
        Order? order = FindOrder(orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        orders.Remove(order);
        processingQueue = new Queue<Order>(processingQueue.Where(currentOrder => currentOrder.OrderId != orderId));
        RefreshCategories();
        Console.WriteLine($"Order {orderId} removed.");
    }

    public void ProcessNextOrder()
    {
        if (processingQueue.Count == 0)
        {
            Console.WriteLine("No orders available for processing.");
            return;
        }

        Order order = processingQueue.Dequeue();
        UpdateStatus(order.OrderId, "Processing");
        UpdateStatus(order.OrderId, "Completed");

        Customer customer = customers[order.CustomerId];
        Console.WriteLine($"Processed order {order.OrderId} for {customer.Name}: {order.ProductName}");
    }

    public void UpdateStatus(int orderId, string status)
    {
        Order? order = FindOrder(orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        order.StatusHistory.Push(status);
    }

    public void DisplayOrders()
    {
        foreach (Order order in orders)
        {
            Customer customer = customers[order.CustomerId];
            Console.WriteLine(
                $"Order: {order.OrderId}, Customer: {customer.Name}, Product: {order.ProductName}, Price: Rs. {order.Price}, Category: {order.Category}, Status: {order.StatusHistory.Peek()}");
        }
    }

    public void DisplayCategories()
    {
        Console.WriteLine("Unique product categories: " + string.Join(", ", categories));
    }

    public void DisplayStatusHistory(int orderId)
    {
        Order? order = FindOrder(orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        Console.WriteLine($"Order {orderId} status history: {string.Join(" -> ", order.StatusHistory)}");
    }

    private Order? FindOrder(int orderId)
    {
        return orders.FirstOrDefault(order => order.OrderId == orderId);
    }

    private void RefreshCategories()
    {
        categories.Clear();

        foreach (Order order in orders)
        {
            categories.Add(order.Category);
        }
    }
}
