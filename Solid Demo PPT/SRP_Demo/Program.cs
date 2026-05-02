using System;

namespace SRP_Demo
{
    // CORRECT: Single Responsibility Principle
    // Each class has only one reason to change.

    class Order
    {
        public void CalculatePrice() 
        { 
            Console.WriteLine("Price calculated for the order."); 
        }
    }

    class OrderPrinter
    {
        public void PrintOrder(Order order) 
        { 
            Console.WriteLine("Order details printed successfully."); 
        }
    }

    class OrderRepository
    {
        public void SaveOrder(Order order) 
        { 
            Console.WriteLine("Order saved to database."); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Single Responsibility Principle (SRP) Demo ===");
            var order = new Order();
            order.CalculatePrice();

            var printer = new OrderPrinter();
            printer.PrintOrder(order);

            var repo = new OrderRepository();
            repo.SaveOrder(order);
        }
    }
}
