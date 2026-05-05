using DigitalWallet.Core.Models;
using DigitalWallet.Core.Notifications;
using DigitalWallet.Core.Payments;
using DigitalWallet.Core.Services;
using DigitalWallet.Core.Transactions;

namespace DigitalWallet.Tests;

[TestClass]
public sealed class WalletServiceTests
{
    [TestMethod]
    public void AddMoney_PositiveAmount_IncreasesWalletBalanceAndStoresTransaction()
    {
        Wallet wallet = new(101, "Amit Sharma", 1000m);
        InMemoryTransactionRepository repository = new();
        WalletService service = CreateService(new UpiPayment(), repository);

        service.AddMoney(wallet, 500m);

        Assert.AreEqual(1500m, wallet.Balance);
        Assert.AreEqual(1, repository.GetByUserId(wallet.UserId).Count);
    }

    [TestMethod]
    public void MakePayment_ValidUpiPayment_DeductsBalanceAndStoresSuccessTransaction()
    {
        Wallet wallet = new(101, "Amit Sharma", 1000m);
        InMemoryTransactionRepository repository = new();
        WalletService service = CreateService(new UpiPayment(), repository);
        PaymentRequest request = new(wallet.UserId, "Amazon India", 300m, "Order payment");

        PaymentResult result = service.MakePayment(wallet, request);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(700m, wallet.Balance);
        Assert.AreEqual("UPI", result.PaymentMethod);
        Assert.AreEqual("SUCCESS", repository.GetByUserId(wallet.UserId).Single().Status);
    }

    [TestMethod]
    public void MakePayment_InsufficientBalance_ReturnsFailedResultAndKeepsBalance()
    {
        Wallet wallet = new(101, "Amit Sharma", 100m);
        InMemoryTransactionRepository repository = new();
        WalletService service = CreateService(new CardPayment(), repository);
        PaymentRequest request = new(wallet.UserId, "Flipkart", 300m, "Order payment");

        PaymentResult result = service.MakePayment(wallet, request);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(100m, wallet.Balance);
        Assert.AreEqual("FAILED", repository.GetByUserId(wallet.UserId).Single().Status);
    }

    [TestMethod]
    public void MakePayment_AllPaymentMethods_CanBeSubstitutedWithoutBreakingWorkflow()
    {
        IPaymentMethod[] paymentMethods =
        [
            new UpiPayment(),
            new CardPayment(),
            new NetBankingPayment(),
            new WalletPayment()
        ];

        foreach (IPaymentMethod paymentMethod in paymentMethods)
        {
            Wallet wallet = new(101, "Amit Sharma", 1000m);
            InMemoryTransactionRepository repository = new();
            WalletService service = CreateService(paymentMethod, repository);
            PaymentRequest request = new(wallet.UserId, "Merchant", 100m, "LSP test");

            PaymentResult result = service.MakePayment(wallet, request);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(paymentMethod.Name, result.PaymentMethod);
            Assert.AreEqual(900m, wallet.Balance);
        }
    }

    private static WalletService CreateService(
        IPaymentMethod paymentMethod,
        ITransactionRepository repository)
    {
        return new WalletService(paymentMethod, new FakeNotificationSender(), repository);
    }

    private sealed class FakeNotificationSender : INotificationSender
    {
        public void SendPaymentStatus(Wallet wallet, PaymentResult result)
        {
        }
    }
}
