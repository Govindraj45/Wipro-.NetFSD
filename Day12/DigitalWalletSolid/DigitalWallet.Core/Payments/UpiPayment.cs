using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Payments;

public sealed class UpiPayment : IPaymentMethod
{
    public string Name => "UPI";

    public PaymentResult Pay(PaymentRequest request)
    {
        return PaymentResult.Success(Name, $"UPI-{request.UserId}-{DateTime.UtcNow:yyyyMMddHHmmss}");
    }
}
