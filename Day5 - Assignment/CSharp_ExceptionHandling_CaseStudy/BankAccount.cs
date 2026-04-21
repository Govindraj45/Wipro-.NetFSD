using System;

namespace CSharp_ExceptionHandling_CaseStudy
{
    public class BankAccount
    {
        public string AccountHolderName { get; private set; }
        public double Balance { get; private set; }

        public BankAccount(string accountHolderName, double initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountHolderName))
            {
                throw new ArgumentException("Account holder name cannot be null or empty.");
            }

            if (initialBalance < 1000)
            {
                throw new InsufficientBalanceException("Initial balance must be at least ₹1000.");
            }

            AccountHolderName = accountHolderName;
            Balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Deposit amount must be greater than 0.");
            }

            Balance += amount;
            Console.WriteLine($"[Success] Deposited ₹{amount}. New Balance: ₹{Balance}");
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be greater than 0.");
            }

            if (amount > Balance)
            {
                throw new InsufficientBalanceException($"Cannot withdraw ₹{amount}. Amount exceeds current balance of ₹{Balance}.");
            }

            if ((Balance - amount) < 1000)
            {
                throw new InsufficientBalanceException($"Cannot withdraw ₹{amount}. Minimum balance of ₹1000 must be maintained.");
            }

            Balance -= amount;
            Console.WriteLine($"[Success] Withdrew ₹{amount}. New Balance: ₹{Balance}");
        }

        public void CheckBalance()
        {
            Console.WriteLine($"[Balance] Account Holder: {AccountHolderName} | Current Balance: ₹{Balance}");
        }
    }
}
