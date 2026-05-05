using DigitalWallet.Core.Models;
using DigitalWallet.Core.Notifications;
using DigitalWallet.Core.Payments;
using DigitalWallet.Core.Services;
using DigitalWallet.Core.Transactions;

using Console = System.Console;

Wallet wallet = new(101, "Amit Sharma", 5000m);
ITransactionRepository transactionRepository = new InMemoryTransactionRepository();
INotificationSender notificationSender = new ConsoleNotificationSender();

List<IPaymentMethod> paymentMethods =
[
    new UpiPayment(),
    new CardPayment(),
    new NetBankingPayment(),
    new WalletPayment()
];

Console.WriteLine("Day 12 SOLID Digital Wallet Demo");
Console.WriteLine($"User: {wallet.OwnerName}");
Console.WriteLine($"Opening balance: {wallet.Balance:C}");
Console.WriteLine();

foreach (IPaymentMethod paymentMethod in paymentMethods)
{
    WalletService walletService = new(paymentMethod, notificationSender, transactionRepository);
    PaymentRequest request = new(wallet.UserId, "Amazon India", 500m, "Order payment");

    PaymentResult result = walletService.MakePayment(wallet, request);

    Console.WriteLine($"{paymentMethod.Name}: {result.Message}");
    Console.WriteLine($"Remaining balance: {wallet.Balance:C}");
    Console.WriteLine();
}

WalletService historyService = new(new UpiPayment(), notificationSender, transactionRepository);

Console.WriteLine("Transaction History");
foreach (TransactionRecord transaction in historyService.ViewTransactionHistory(wallet.UserId))
{
    Console.WriteLine($"{transaction.CreatedAt:u} | {transaction.PaymentMethod} | {transaction.MerchantName} | {transaction.Amount:C} | {transaction.Status}");
}
