using System;
using System.Collections.Generic;
using System.IO;

static class ExceptionFileHandlingDemo
{
    public static void Run()
    {
        ConsoleOutput.WriteHeading("Exception and File Handling");

        string filePath = Path.Combine(Environment.CurrentDirectory, "day5-orders.txt");

        try
        {
            List<string> orderLines = new()
            {
                "1001,Laptop,75000",
                "1002,Mouse,800",
                "InvalidLineForDemo"
            };

            File.WriteAllLines(filePath, orderLines);
            Console.WriteLine($"File created: {filePath}");

            string[] savedOrders = File.ReadAllLines(filePath);
            foreach (string line in savedOrders)
            {
                try
                {
                    FileOrder order = FileOrderParser.Parse(line);
                    Console.WriteLine($"Parsed order: {order.OrderId}, {order.ProductName}, Rs. {order.Price}");
                }
                catch (FormatException exception)
                {
                    Console.WriteLine($"Skipped invalid line: {exception.Message}");
                }
            }
        }
        catch (IOException exception)
        {
            Console.WriteLine($"File handling error: {exception.Message}");
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.WriteLine($"Permission error: {exception.Message}");
        }
        finally
        {
            Console.WriteLine("File handling demo completed.");
        }
    }
}

static class FileOrderParser
{
    public static FileOrder Parse(string line)
    {
        string[] parts = line.Split(',');
        if (parts.Length != 3)
        {
            throw new FormatException($"'{line}' does not contain OrderId, ProductName, and Price.");
        }

        if (!int.TryParse(parts[0], out int orderId))
        {
            throw new FormatException($"'{parts[0]}' is not a valid order id.");
        }

        if (!decimal.TryParse(parts[2], out decimal price))
        {
            throw new FormatException($"'{parts[2]}' is not a valid price.");
        }

        return new FileOrder(orderId, parts[1], price);
    }
}

class FileOrder
{
    public FileOrder(int orderId, string productName, decimal price)
    {
        OrderId = orderId;
        ProductName = productName;
        Price = price;
    }

    public int OrderId { get; }
    public string ProductName { get; }
    public decimal Price { get; }
}
