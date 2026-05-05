namespace DigitalWallet.Core.Models;

public sealed record TransactionRecord(
    string TransactionId,
    int UserId,
    string MerchantName,
    decimal Amount,
    string PaymentMethod,
    string Status,
    DateTime CreatedAt);
