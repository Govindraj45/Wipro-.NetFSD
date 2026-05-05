namespace DigitalWallet.Core.Models;

public sealed record PaymentRequest(
    int UserId,
    string MerchantName,
    decimal Amount,
    string Description);
