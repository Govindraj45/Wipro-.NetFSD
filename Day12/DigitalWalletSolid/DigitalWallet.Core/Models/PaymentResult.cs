namespace DigitalWallet.Core.Models;

public sealed record PaymentResult(
    bool IsSuccess,
    string PaymentMethod,
    string ReferenceNumber,
    string Message)
{
    public static PaymentResult Success(string paymentMethod, string referenceNumber)
    {
        return new PaymentResult(true, paymentMethod, referenceNumber, "Payment completed successfully.");
    }

    public static PaymentResult Failed(string paymentMethod, string message)
    {
        return new PaymentResult(false, paymentMethod, string.Empty, message);
    }
}
