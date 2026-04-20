namespace Day4PracticeAssignmentApp.Scenarios.Banking;

interface IBankingService
{
    void AddAccount(string accountNumber, double openingBalance);

    void QueueTransaction(Transaction transaction);

    void ProcessPendingTransactions();

    void RollbackLastTransaction();

    void DisplayBalances();

    void DisplayTransactionHistory();
}
