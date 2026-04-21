using System;
using System.Threading.Tasks;

namespace Day5AdvancedCSharpDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("     Advanced C# Concepts Demo");
                Console.WriteLine("========================================");
                Console.WriteLine("1. C# 7 & 8 Features Demo");
                Console.WriteLine("2. Exception & File Handling Demo");
                Console.WriteLine("3. Delegates & Events Demo");
                Console.WriteLine("4. Reflection & Attributes Demo");
                Console.WriteLine("5. SOLID Principles & Design Patterns");
                Console.WriteLine("0. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        await Features7And8Demo.RunDemoAsync();
                        break;
                    case "2":
                        ExceptionFileHandlingDemo.RunDemo();
                        break;
                    case "3":
                        DelegatesEventsDemo.RunDemo();
                        break;
                    case "4":
                        ReflectionAttributesDemo.RunDemo();
                        break;
                    case "5":
                        SolidDesignPatternsDemo.RunDemo();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting demo. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
