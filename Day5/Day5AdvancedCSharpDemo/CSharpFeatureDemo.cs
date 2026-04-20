using System;
using System.Collections.Generic;
using System.Threading.Tasks;

static class CSharpFeatureDemo
{
    public static async Task RunAsync()
    {
        ConsoleOutput.WriteHeading("Top 10 C# 7 and C# 8 Features");

        (string language, int versionsCovered) = GetLanguageSummary();
        Console.WriteLine($"1. Tuple literal: {language}, versions covered: {versionsCovered}");

        if (int.TryParse("25", out int orderCount))
        {
            Console.WriteLine($"2. Out variable: parsed order count is {orderCount}");
        }

        object orderAmount = 1500.75m;
        if (orderAmount is decimal amount)
        {
            Console.WriteLine($"3. Pattern matching: amount is a decimal value of Rs. {amount}");
        }

        int factorial = CalculateFactorial(5);
        Console.WriteLine($"4. Local function: factorial of 5 is {factorial}");

        (int orderId, string status) = GetOrderStatus();
        Console.WriteLine($"5. Deconstruction: order {orderId} status is {status}");

        string priority = orderCount switch
        {
            <= 10 => "Low",
            <= 50 => "Medium",
            _ => "High"
        };
        Console.WriteLine($"6. Switch expression: order priority is {priority}");

        string? customerName = null;
        customerName ??= "Guest Customer";
        Console.WriteLine($"7. Nullable reference and null-coalescing assignment: {customerName}");

        using DemoResource resource = new("Day 5 audit resource");
        Console.WriteLine($"8. Using declaration: {resource.Name} will be disposed automatically");

        string[] products = { "Laptop", "Mouse", "Keyboard", "Monitor", "Printer" };
        Console.WriteLine($"9. Indices and ranges: last product is {products[^1]}");
        Console.WriteLine($"   Middle products are {string.Join(", ", products[1..4])}");

        Console.WriteLine("10. Async stream:");
        await foreach (int number in GetAsyncOrderNumbers())
        {
            Console.WriteLine($"    Received async order number {number}");
        }

        IOrderFormatter formatter = new BasicOrderFormatter();
        Console.WriteLine($"Bonus C# 8 default interface method: {formatter.FormatShort(orderId)}");

        static int CalculateFactorial(int number)
        {
            return number <= 1 ? 1 : number * CalculateFactorial(number - 1);
        }
    }

    private static (string language, int versionsCovered) GetLanguageSummary()
    {
        return ("C# 7 and C# 8", 2);
    }

    private static (int orderId, string status) GetOrderStatus()
    {
        return (1001, "Confirmed");
    }

    private static async IAsyncEnumerable<int> GetAsyncOrderNumbers()
    {
        for (int orderNumber = 1; orderNumber <= 3; orderNumber++)
        {
            await Task.Delay(10);
            yield return orderNumber;
        }
    }
}

class DemoResource : IDisposable
{
    public DemoResource(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Dispose()
    {
        Console.WriteLine($"    Disposed resource: {Name}");
    }
}

interface IOrderFormatter
{
    string Format(int orderId);

    string FormatShort(int orderId)
    {
        return $"ORD-{orderId}";
    }
}

class BasicOrderFormatter : IOrderFormatter
{
    public string Format(int orderId)
    {
        return $"Order number {orderId}";
    }
}
