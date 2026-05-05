using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Payments;

public interface IPaymentMethod
{
    string Name { get; }

    PaymentResult Pay(PaymentRequest request);
}
