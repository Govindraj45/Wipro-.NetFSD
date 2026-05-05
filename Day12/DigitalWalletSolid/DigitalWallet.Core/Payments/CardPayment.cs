using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Payments;

public sealed class CardPayment : IPaymentMethod
{
    public string Name => "Card";

    public PaymentResult Pay(PaymentRequest request)
    {
        return PaymentResult.Success(Name, $"CARD-{request.UserId}-{DateTime.UtcNow:yyyyMMddHHmmss}");
    }
}
