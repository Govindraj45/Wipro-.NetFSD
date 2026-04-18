using System;
using System.Collections.Generic;
using System.Linq;

class OrderService
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
            Console.WriteLine($"Customer {order.CustomerId} does not exist.");
            return;
        }

        order.StatusHistory.Push("Placed");
        orders.Add(order);
        categories.Add(order.Category);
        processingQueue.Enqueue(order);
    }

    public void UpdateOrder(int orderId, string productName, double price, string category)
    {
        Order? order = orders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
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
        Order? order = orders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        orders.Remove(order);
        processingQueue = new Queue<Order>(
            processingQueue.Where(currentOrder => currentOrder.OrderId != orderId));
        RefreshCategories();
        Console.WriteLine($"Order {orderId} removed.");
        Console.WriteLine();
    }

    public void ProcessNextOrder()
    {
        if (processingQueue.Count == 0)
        {
            Console.WriteLine("No orders left to process.");
            Console.WriteLine();
            return;
        }

        Order order = processingQueue.Dequeue();
        UpdateStatus(order.OrderId, "Processing");
        UpdateStatus(order.OrderId, "Completed");

        Customer customer = customers[order.CustomerId];
        Console.WriteLine(
            $"Processed Order {order.OrderId} for {customer.Name} - {order.ProductName} - Rs. {order.Price}");
        Console.WriteLine();
    }

    public void UpdateStatus(int orderId, string status)
    {
        Order? order = orders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
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
                $"Order ID: {order.OrderId}, Customer: {customer.Name}, Product: {order.ProductName}, Price: Rs. {order.Price}, Category: {order.Category}, Current Status: {order.StatusHistory.Peek()}");
        }

        Console.WriteLine();
    }

    public void DisplayCategories()
    {
        Console.WriteLine("Unique Product Categories:");
        foreach (string category in categories)
        {
            Console.WriteLine(category);
        }

        Console.WriteLine();
    }

    public void DisplayStatusHistory(int orderId)
    {
        Order? order = orders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine($"Order {orderId} Status History:");
        foreach (string status in order.StatusHistory)
        {
            Console.WriteLine(status);
        }

        Console.WriteLine();
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
