using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Notifications;

public interface INotificationSender
{
    void SendPaymentStatus(Wallet wallet, PaymentResult result);
}
