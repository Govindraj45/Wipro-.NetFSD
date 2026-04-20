using Day4PracticeAssignmentApp.Common;

namespace Day4PracticeAssignmentApp.Scenarios.Banking;

class BankingTransactionDemo : IScenarioDemo
{
    public string Title => "Scenario 3: Banking Transaction System";

    public void Run()
    {
        IBankingService bankingService = new BankingService();

        bankingService.AddAccount("ACC1001", 10000);
        bankingService.AddAccount("ACC1002", 5000);

        bankingService.QueueTransaction(new Transaction("TXN001", "ACC1001", 2500, TransactionType.Deposit));
        bankingService.QueueTransaction(new Transaction("TXN002", "ACC1002", 1200, TransactionType.Withdraw));
        bankingService.QueueTransaction(new Transaction("TXN002", "ACC1002", 500, TransactionType.Withdraw));

        ConsoleOutput.WriteSubHeading("Process Pending Transactions");
        bankingService.ProcessPendingTransactions();

        ConsoleOutput.WriteSubHeading("Account Balances");
        bankingService.DisplayBalances();

        ConsoleOutput.WriteSubHeading("Rollback Last Transaction");
        bankingService.RollbackLastTransaction();
        bankingService.DisplayBalances();

        ConsoleOutput.WriteSubHeading("Transaction History");
        bankingService.DisplayTransactionHistory();
    }
}
