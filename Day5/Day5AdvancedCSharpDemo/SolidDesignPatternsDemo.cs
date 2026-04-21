using System;

namespace Day5AdvancedCSharpDemo
{
    // --- SOLID Principles Demo ---
    // 1. Single Responsibility Principle (SRP)
    // A class should have one, and only one, reason to change.
    public class ReportGenerator
    {
        public string GenerateReport()
        {
            return "Annual Financial Report Content...";
        }
    }

    public class ReportSaver
    {
        public void SaveToFile(string content, string filename)
        {
            Console.WriteLine($"[SRP] Saving report to {filename}");
        }
    }

    // 2. Dependency Inversion Principle (DIP)
    // Depend upon abstractions, not concretions.
    public interface IMessageService
    {
        void SendMessage(string message);
    }

    public class EmailService : IMessageService
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"[DIP] Sending Email: {message}");
        }
    }

    public class NotificationSender
    {
        private readonly IMessageService _messageService;

        public NotificationSender(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Notify(string msg)
        {
            _messageService.SendMessage(msg);
        }
    }

    // --- Design Patterns Demo ---
    // 3. Factory Method Design Pattern
    public interface IAnimal
    {
        void Speak();
    }

    public class Dog : IAnimal
    {
        public void Speak() => Console.WriteLine("[Factory] Dog says: Woof!");
    }

    public class Cat : IAnimal
    {
        public void Speak() => Console.WriteLine("[Factory] Cat says: Meow!");
    }

    public class AnimalFactory
    {
        public static IAnimal CreateAnimal(string type)
        {
            return type.ToLower() switch
            {
                "dog" => new Dog(),
                "cat" => new Cat(),
                _ => throw new ArgumentException("Unknown animal type")
            };
        }
    }

    public class SolidDesignPatternsDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("--- SOLID Principles & Design Patterns Demo ---\n");

            Console.WriteLine(">> 1. Single Responsibility Principle (SRP)");
            var generator = new ReportGenerator();
            var saver = new ReportSaver();
            string report = generator.GenerateReport();
            saver.SaveToFile(report, "report.pdf");

            Console.WriteLine("\n>> 2. Dependency Inversion Principle (DIP)");
            IMessageService emailService = new EmailService();
            NotificationSender sender = new NotificationSender(emailService);
            sender.Notify("System is down for maintenance.");

            Console.WriteLine("\n>> 3. Factory Design Pattern");
            IAnimal myDog = AnimalFactory.CreateAnimal("dog");
            myDog.Speak();

            IAnimal myCat = AnimalFactory.CreateAnimal("cat");
            myCat.Speak();

            Console.WriteLine();
        }
    }
}
