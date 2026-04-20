namespace Day4PracticeAssignmentApp.Scenarios.Banking;

class BankingService : IBankingService
{
    private readonly List<Transaction> transactionHistory = new();
    private readonly Dictionary<string, double> accountBalances = new();
    private readonly Queue<Transaction> pendingTransactions = new();
    private readonly Stack<Transaction> rollbackStack = new();
    private readonly HashSet<string> transactionIds = new(StringComparer.OrdinalIgnoreCase);

    public void AddAccount(string accountNumber, double openingBalance)
    {
        accountBalances[accountNumber] = openingBalance;
    }

    public void QueueTransaction(Transaction transaction)
    {
        if (!transactionIds.Add(transaction.TransactionId))
        {
            Console.WriteLine($"Duplicate transaction blocked: {transaction.TransactionId}");
            return;
        }

        pendingTransactions.Enqueue(transaction);
    }

    public void ProcessPendingTransactions()
    {
        while (pendingTransactions.Count > 0)
        {
            Transaction transaction = pendingTransactions.Dequeue();
            if (!accountBalances.ContainsKey(transaction.AccountNumber))
            {
                Console.WriteLine($"Account not found: {transaction.AccountNumber}");
                continue;
            }

            if (transaction.TransactionType == TransactionType.Withdraw &&
                accountBalances[transaction.AccountNumber] < transaction.Amount)
            {
                Console.WriteLine($"Insufficient balance for transaction {transaction.TransactionId}");
                continue;
            }

            ApplyTransaction(transaction);
            transactionHistory.Add(transaction);
            rollbackStack.Push(transaction);
            Console.WriteLine($"Processed {transaction.TransactionId}: {transaction.TransactionType} Rs. {transaction.Amount}");
        }
    }

    public void RollbackLastTransaction()
    {
        if (rollbackStack.Count == 0)
        {
            Console.WriteLine("No transaction available for rollback.");
            return;
        }

        Transaction transaction = rollbackStack.Pop();
        ReverseTransaction(transaction);
        transactionHistory.Remove(transaction);
        transactionIds.Remove(transaction.TransactionId);
        Console.WriteLine($"Rolled back transaction {transaction.TransactionId}");
    }

    public void DisplayBalances()
    {
        foreach (KeyValuePair<string, double> account in accountBalances)
        {
            Console.WriteLine($"Account: {account.Key}, Balance: Rs. {account.Value}");
        }
    }

    public void DisplayTransactionHistory()
    {
        foreach (Transaction transaction in transactionHistory)
        {
            Console.WriteLine(
                $"{transaction.TransactionId}: {transaction.TransactionType} Rs. {transaction.Amount} on {transaction.AccountNumber}");
        }
    }

    private void ApplyTransaction(Transaction transaction)
    {
        if (transaction.TransactionType == TransactionType.Deposit)
        {
            accountBalances[transaction.AccountNumber] += transaction.Amount;
            return;
        }

        accountBalances[transaction.AccountNumber] -= transaction.Amount;
    }

    private void ReverseTransaction(Transaction transaction)
    {
        if (transaction.TransactionType == TransactionType.Deposit)
        {
            accountBalances[transaction.AccountNumber] -= transaction.Amount;
            return;
        }

        accountBalances[transaction.AccountNumber] += transaction.Amount;
    }
}
