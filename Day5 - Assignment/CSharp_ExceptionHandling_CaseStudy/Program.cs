using System;

namespace CSharp_ExceptionHandling_CaseStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(" Banking Transaction System");
            Console.WriteLine("========================================");

            BankAccount account = null;

            try
            {
                // Initialize the account
                account = new BankAccount("John Doe", 5000);
                account.CheckBalance();

                Console.WriteLine("\n--- Scenario 1: Valid Deposit ---");
                account.Deposit(2000);

                Console.WriteLine("\n--- Scenario 2: Valid Withdraw ---");
                account.Withdraw(1500);

                Console.WriteLine("\n--- Scenario 3: Deposit <= 0 ---");
                account.Deposit(-500); // Should throw InvalidAmountException
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine($"[Error - Invalid Amount]: {ex.Message}");
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"[Error - Insufficient Balance]: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Error - Invalid Argument]: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[Error - Invalid Operation]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error - System]: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Transaction attempt finished.");
            }

            try
            {
                Console.WriteLine("\n--- Scenario 4: Withdraw > balance ---");
                account.Withdraw(10000); // Should throw InsufficientBalanceException
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"[Error - Insufficient Balance]: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Transaction attempt finished.");
            }

            try
            {
                Console.WriteLine("\n--- Scenario 5: Withdraw causing balance < 1000 ---");
                account.Withdraw(4800); // Balance is currently 5500. 5500 - 4800 = 700. Should throw InsufficientBalanceException
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"[Error - Insufficient Balance]: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Transaction attempt finished.");
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine(" End of Banking Operations");
            Console.WriteLine("========================================");
        }
    }
}
