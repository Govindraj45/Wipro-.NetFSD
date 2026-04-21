using System;

delegate decimal DiscountCalculator(decimal amount);

static class DelegateEventDemo
{
    public static void Run()
    {
        ConsoleOutput.WriteHeading("Delegates and Events");

        DiscountCalculator festivalDiscount = amount => amount * 0.10m;
        PaymentProcessor processor = new();

        processor.PaymentProcessed += OnPaymentProcessed;
        processor.ProcessPayment(5000m, festivalDiscount);
        processor.PaymentProcessed -= OnPaymentProcessed;
    }

    private static void OnPaymentProcessed(object? sender, PaymentProcessedEventArgs eventArgs)
    {
        Console.WriteLine(
            $"Event received: order {eventArgs.OrderId} paid Rs. {eventArgs.FinalAmount} after Rs. {eventArgs.DiscountAmount} discount.");
    }
}

class PaymentProcessor
{
    public event EventHandler<PaymentProcessedEventArgs>? PaymentProcessed;

    public void ProcessPayment(decimal amount, DiscountCalculator discountCalculator)
    {
        decimal discountAmount = discountCalculator(amount);
        decimal finalAmount = amount - discountAmount;

        Console.WriteLine($"Delegate calculated discount: Rs. {discountAmount}");
        PaymentProcessed?.Invoke(this, new PaymentProcessedEventArgs(1001, finalAmount, discountAmount));
    }
}

class PaymentProcessedEventArgs : EventArgs
{
    public PaymentProcessedEventArgs(int orderId, decimal finalAmount, decimal discountAmount)
    {
        OrderId = orderId;
        FinalAmount = finalAmount;
        DiscountAmount = discountAmount;
    }

    public int OrderId { get; }
    public decimal FinalAmount { get; }
    public decimal DiscountAmount { get; }
}
