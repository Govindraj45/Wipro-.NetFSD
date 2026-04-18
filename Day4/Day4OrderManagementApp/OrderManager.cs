using System;
using System.Collections.Generic;
using System.Linq;

class OrderManager
{
    private readonly List<Order> allOrders = new();
    private readonly Dictionary<int, Customer> customers = new();
    private readonly HashSet<string> productCategories = new(StringComparer.OrdinalIgnoreCase);
    private Queue<Order> orderProcessingQueue = new();

    public bool HasOrdersToProcess => orderProcessingQueue.Count > 0;

    public void AddCustomer(Customer customer)
    {
        customers[customer.CustomerId] = customer;
    }

    public void AddOrder(Order order)
    {
        if (!customers.ContainsKey(order.CustomerId))
        {
            Console.WriteLine($"Customer {order.CustomerId} not found. Order {order.OrderId} was not added.");
            return;
        }

        allOrders.Add(order);
        productCategories.Add(order.Category);
        order.StatusHistory.Push("Placed");
        orderProcessingQueue.Enqueue(order);
    }

    public void UpdateOrder(int orderId, string newProductName, string newCategory, decimal newAmount)
    {
        Order? order = allOrders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        order.ProductName = newProductName;
        order.Category = newCategory;
        order.TotalAmount = newAmount;
        UpdateOrderStatus(orderId, "Updated");
        RefreshProductCategories();
    }

    public void RemoveOrder(int orderId)
    {
        Order? order = allOrders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        allOrders.Remove(order);
        orderProcessingQueue = new Queue<Order>(
            orderProcessingQueue.Where(currentOrder => currentOrder.OrderId != orderId));
        RefreshProductCategories();
        Console.WriteLine($"Order {orderId} removed successfully.");
        Console.WriteLine();
    }

    public void UpdateOrderStatus(int orderId, string newStatus)
    {
        Order? order = allOrders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found.");
            return;
        }

        order.StatusHistory.Push(newStatus);
    }

    public void ProcessNextOrder()
    {
        if (orderProcessingQueue.Count == 0)
        {
            Console.WriteLine("No orders left to process.");
            Console.WriteLine();
            return;
        }

        Order order = orderProcessingQueue.Dequeue();
        UpdateOrderStatus(order.OrderId, "Processing");
        UpdateOrderStatus(order.OrderId, "Completed");

        Customer customer = customers[order.CustomerId];
        Console.WriteLine(
            $"Processed Order {order.OrderId} for {customer.Name} - {order.ProductName} ({order.Category}) - Rs. {order.TotalAmount}");
        Console.WriteLine();
    }

    public void DisplayOrders()
    {
        foreach (Order order in allOrders)
        {
            Customer customer = customers[order.CustomerId];
            string currentStatus = order.StatusHistory.Peek();
            Console.WriteLine(
                $"Order ID: {order.OrderId}, Customer: {customer.Name}, Product: {order.ProductName}, Category: {order.Category}, Amount: Rs. {order.TotalAmount}, Current Status: {currentStatus}");
        }

        Console.WriteLine();
    }

    public void DisplayUniqueProductCategories()
    {
        Console.WriteLine("Unique Product Categories:");
        foreach (string category in productCategories)
        {
            Console.WriteLine(category);
        }

        Console.WriteLine();
    }

    public void DisplayStatusHistory(int orderId)
    {
        Order? order = allOrders.FirstOrDefault(currentOrder => currentOrder.OrderId == orderId);
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

    private void RefreshProductCategories()
    {
        productCategories.Clear();

        foreach (Order order in allOrders)
        {
            productCategories.Add(order.Category);
        }
    }
}
