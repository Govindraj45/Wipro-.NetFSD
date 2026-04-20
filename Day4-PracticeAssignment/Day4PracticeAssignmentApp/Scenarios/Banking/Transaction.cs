namespace Day4PracticeAssignmentApp.Scenarios.Banking;

class Transaction
{
    public Transaction(string transactionId, string accountNumber, double amount, TransactionType transactionType)
    {
        TransactionId = transactionId;
        AccountNumber = accountNumber;
        Amount = amount;
        TransactionType = transactionType;
    }

    public string TransactionId { get; }
    public string AccountNumber { get; }
    public double Amount { get; }
    public TransactionType TransactionType { get; }
}
