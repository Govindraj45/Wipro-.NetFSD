using OrderApp;
using OrderApp.Services;

OrderProcessor processor = new();
EmailService emailService = new();
SMSService smsService = new();
LoggerService loggerService = new();

// Subscribe multiple modules. This creates multicast delegate behavior.
processor.OnOrderPlaced += emailService.SendEmail;
processor.OnOrderPlaced += smsService.SendSMS;
processor.OnOrderPlaced += loggerService.LogOrder;

Order firstOrder = new(101, "Govindraj", 4999.50);
processor.ProcessOrder(firstOrder);

Console.WriteLine();
Console.WriteLine("Unsubscribing SMS service...");
processor.OnOrderPlaced -= smsService.SendSMS;

Order secondOrder = new(102, "Aarav", 2499.00);
processor.ProcessOrder(secondOrder);

Console.WriteLine();
Console.WriteLine("Action<T> bonus delegate demo:");
processor.ProcessOrderWithAction(new Order(103, "Diya", 999.00), order =>
{
    Console.WriteLine($"Action delegate notified admin for Order {order.OrderId}");
});
