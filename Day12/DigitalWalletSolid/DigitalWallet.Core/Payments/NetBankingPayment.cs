using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Payments;

public sealed class NetBankingPayment : IPaymentMethod
{
    public string Name => "Net Banking";

    public PaymentResult Pay(PaymentRequest request)
    {
        return PaymentResult.Success(Name, $"NET-{request.UserId}-{DateTime.UtcNow:yyyyMMddHHmmss}");
    }
}
