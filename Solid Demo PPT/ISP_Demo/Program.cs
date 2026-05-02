using System;

namespace ISP_Demo
{
    // The Rule: No class should be forced to implement interfaces it doesn't use.
    // So we split large fat interfaces into smaller ones.

    public interface IPrinter
    {
        void Print();
    }

    public interface IScanner
    {
        void Scan();
    }

    public interface IFax
    {
        void Fax();
    }

    public class BasicPrinter : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("BasicPrinter is printing a document...");
        }
    }

    public class AdvancedPrinter : IPrinter, IScanner, IFax
    {
        public void Print() => Console.WriteLine("AdvancedPrinter is printing in high quality...");
        public void Scan() => Console.WriteLine("AdvancedPrinter is scanning a document...");
        public void Fax() => Console.WriteLine("AdvancedPrinter is sending a fax...");
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Interface Segregation Principle (ISP) Demo ===");

            IPrinter basic = new BasicPrinter();
            basic.Print();
            // We never expose Scan() or Fax() to BasicPrinter, keeping it clean.

            Console.WriteLine("\nAdvanced Printer Features:");
            var advanced = new AdvancedPrinter();
            advanced.Print();
            advanced.Scan();
            advanced.Fax();
        }
    }
}
