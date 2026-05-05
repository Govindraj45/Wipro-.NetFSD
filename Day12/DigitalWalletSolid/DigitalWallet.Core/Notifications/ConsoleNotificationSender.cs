using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Notifications;

public sealed class ConsoleNotificationSender : INotificationSender
{
    public void SendPaymentStatus(Wallet wallet, PaymentResult result)
    {
        Console.WriteLine($"Notification for {wallet.OwnerName}: {result.Message} Method: {result.PaymentMethod}");
    }
}
