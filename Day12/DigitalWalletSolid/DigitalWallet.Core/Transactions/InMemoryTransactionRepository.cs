using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Transactions;

public sealed class InMemoryTransactionRepository : ITransactionRepository
{
    private readonly List<TransactionRecord> _transactions = [];

    public void Save(TransactionRecord transaction)
    {
        _transactions.Add(transaction);
    }

    public IReadOnlyList<TransactionRecord> GetByUserId(int userId)
    {
        return _transactions
            .Where(transaction => transaction.UserId == userId)
            .OrderByDescending(transaction => transaction.CreatedAt)
            .ToList();
    }
}
