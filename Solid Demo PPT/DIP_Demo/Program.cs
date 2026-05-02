using System;

namespace DIP_Demo
{
    // The Rule: Depend on abstractions, not on concrete implementations.

    public interface IPayment
    {
        void Pay(double amount);
    }

    public class CreditCard : IPayment
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"Payment of ${amount} completed using Credit Card.");
        }
    }

    public class UPI : IPayment
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"Payment of ${amount} completed using UPI App.");
        }
    }

    public class PaymentService
    {
        private readonly IPayment _payment;

        // Dependency is injected via constructor (abstraction)
        public PaymentService(IPayment payment)
        {
            _payment = payment;
        }

        public void MakePayment(double amt)
        {
            _payment.Pay(amt);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Dependency Inversion Principle (DIP) Demo ===");

            // Client decides which concrete implementation to inject
            IPayment creditCardMethod = new CreditCard();
            var service1 = new PaymentService(creditCardMethod);
            service1.MakePayment(250.75);

            IPayment upiMethod = new UPI();
            var service2 = new PaymentService(upiMethod);
            service2.MakePayment(45.00);
        }
    }
}
