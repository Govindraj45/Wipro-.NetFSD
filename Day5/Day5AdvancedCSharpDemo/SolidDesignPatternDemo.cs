using System;
using System.Collections.Generic;

static class SolidDesignPatternDemo
{
    public static void Run()
    {
        ConsoleOutput.WriteHeading("SOLID Principles and Design Patterns");
        PrintSolidTable();
        RunStrategyPatternDemo();
        RunFactoryPatternDemo();
        RunDependencyInversionDemo();
    }

    private static void PrintSolidTable()
    {
        List<PrincipleRow> principles = new()
        {
            new PrincipleRow("S", "Single Responsibility", "A class should have one reason to change."),
            new PrincipleRow("O", "Open Closed", "Classes should be open for extension and closed for modification."),
            new PrincipleRow("L", "Liskov Substitution", "Child classes should be usable wherever parent classes are expected."),
            new PrincipleRow("I", "Interface Segregation", "Prefer small focused interfaces over large general interfaces."),
            new PrincipleRow("D", "Dependency Inversion", "Depend on abstractions, not concrete classes.")
        };

        ConsoleOutput.WriteSubHeading("SOLID Table");
        Console.WriteLine($"{"Letter",-8} {"Principle",-24} Description");
        Console.WriteLine(new string('-', 80));

        foreach (PrincipleRow principle in principles)
        {
            Console.WriteLine($"{principle.Letter,-8} {principle.Name,-24} {principle.Description}");
        }
    }

    private static void RunStrategyPatternDemo()
    {
        ConsoleOutput.WriteSubHeading("Strategy Pattern Demo");

        DiscountContext context = new(new RegularCustomerDiscount());
        Console.WriteLine($"Regular customer final price: Rs. {context.GetFinalPrice(10000m)}");

        context.SetStrategy(new PremiumCustomerDiscount());
        Console.WriteLine($"Premium customer final price: Rs. {context.GetFinalPrice(10000m)}");
    }

    private static void RunFactoryPatternDemo()
    {
        ConsoleOutput.WriteSubHeading("Factory Pattern Demo");

        IPaymentGateway paymentGateway = PaymentGatewayFactory.Create("upi");
        paymentGateway.Pay(2500m);

        IPaymentGateway cardGateway = PaymentGatewayFactory.Create("card");
        cardGateway.Pay(4000m);
    }

    private static void RunDependencyInversionDemo()
    {
        ConsoleOutput.WriteSubHeading("Dependency Inversion Demo");

        INotificationSender sender = new EmailNotificationSender();
        OrderNotificationService notificationService = new(sender);
        notificationService.Notify("Order 1001 has been shipped.");
    }
}

class PrincipleRow
{
    public PrincipleRow(string letter, string name, string description)
    {
        Letter = letter;
        Name = name;
        Description = description;
    }

    public string Letter { get; }
    public string Name { get; }
    public string Description { get; }
}

interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount);
}

class RegularCustomerDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount - (amount * 0.05m);
    }
}

class PremiumCustomerDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount - (amount * 0.15m);
    }
}

class DiscountContext
{
    private IDiscountStrategy discountStrategy;

    public DiscountContext(IDiscountStrategy discountStrategy)
    {
        this.discountStrategy = discountStrategy;
    }

    public void SetStrategy(IDiscountStrategy newStrategy)
    {
        discountStrategy = newStrategy;
    }

    public decimal GetFinalPrice(decimal amount)
    {
        return discountStrategy.ApplyDiscount(amount);
    }
}

interface IPaymentGateway
{
    void Pay(decimal amount);
}

class UpiPaymentGateway : IPaymentGateway
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid Rs. {amount} using UPI.");
    }
}

class CardPaymentGateway : IPaymentGateway
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid Rs. {amount} using card.");
    }
}

static class PaymentGatewayFactory
{
    public static IPaymentGateway Create(string paymentMode)
    {
        return paymentMode.ToLower() switch
        {
            "upi" => new UpiPaymentGateway(),
            "card" => new CardPaymentGateway(),
            _ => throw new ArgumentException("Unsupported payment mode.")
        };
    }
}

interface INotificationSender
{
    void Send(string message);
}

class EmailNotificationSender : INotificationSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}

class OrderNotificationService
{
    private readonly INotificationSender notificationSender;

    public OrderNotificationService(INotificationSender notificationSender)
    {
        this.notificationSender = notificationSender;
    }

    public void Notify(string message)
    {
        notificationSender.Send(message);
    }
}
