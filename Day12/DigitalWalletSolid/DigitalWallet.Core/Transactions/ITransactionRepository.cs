using DigitalWallet.Core.Models;

namespace DigitalWallet.Core.Transactions;

public interface ITransactionRepository
{
    void Save(TransactionRecord transaction);

    IReadOnlyList<TransactionRecord> GetByUserId(int userId);
}
