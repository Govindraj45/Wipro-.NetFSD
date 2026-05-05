using DigitalWallet.Core.Models;
using DigitalWallet.Core.Notifications;
using DigitalWallet.Core.Payments;
using DigitalWallet.Core.Transactions;

namespace DigitalWallet.Core.Services;

public sealed class WalletService
{
    private readonly IPaymentMethod _paymentMethod;
    private readonly INotificationSender _notificationSender;
    private readonly ITransactionRepository _transactionRepository;

    public WalletService(
        IPaymentMethod paymentMethod,
        INotificationSender notificationSender,
        ITransactionRepository transactionRepository)
    {
        _paymentMethod = paymentMethod;
        _notificationSender = notificationSender;
        _transactionRepository = transactionRepository;
    }

    public PaymentResult MakePayment(Wallet wallet, PaymentRequest request)
    {
        if (wallet.UserId != request.UserId)
        {
            throw new InvalidOperationException("Payment request does not belong to this wallet.");
        }

        try
        {
            wallet.Deduct(request.Amount);

            PaymentResult result = _paymentMethod.Pay(request);
            SaveTransaction(wallet, request, result);
            _notificationSender.SendPaymentStatus(wallet, result);

            return result;
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException or InvalidOperationException)
        {
            PaymentResult failedResult = PaymentResult.Failed(_paymentMethod.Name, ex.Message);
            SaveTransaction(wallet, request, failedResult);
            _notificationSender.SendPaymentStatus(wallet, failedResult);

            return failedResult;
        }
    }

    public void AddMoney(Wallet wallet, decimal amount)
    {
        wallet.AddMoney(amount);

        _transactionRepository.Save(new TransactionRecord(
            Guid.NewGuid().ToString("N"),
            wallet.UserId,
            "Wallet Top-up",
            amount,
            "Wallet",
            "SUCCESS",
            DateTime.UtcNow));
    }

    public IReadOnlyList<TransactionRecord> ViewTransactionHistory(int userId)
    {
        return _transactionRepository.GetByUserId(userId);
    }

    private void SaveTransaction(Wallet wallet, PaymentRequest request, PaymentResult result)
    {
        _transactionRepository.Save(new TransactionRecord(
            string.IsNullOrWhiteSpace(result.ReferenceNumber) ? Guid.NewGuid().ToString("N") : result.ReferenceNumber,
            wallet.UserId,
            request.MerchantName,
            request.Amount,
            result.PaymentMethod,
            result.IsSuccess ? "SUCCESS" : "FAILED",
            DateTime.UtcNow));
    }
}
