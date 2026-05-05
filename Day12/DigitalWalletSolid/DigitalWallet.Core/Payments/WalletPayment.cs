using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Payments;

public sealed class WalletPayment : IPaymentMethod
{
    public string Name => "Wallet";

    public PaymentResult Pay(PaymentRequest request)
    {
        return PaymentResult.Success(Name, $"WALLET-{request.UserId}-{DateTime.UtcNow:yyyyMMddHHmmss}");
    }
}
