using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day5AdvancedCSharpDemo
{
    public class Features7And8Demo
    {
        public static async Task RunDemoAsync()
        {
            Console.WriteLine("--- Top 10 C# 7 & 8 Features Demo ---\n");

            // 1. out variables
            string numberStr = "123";
            if (int.TryParse(numberStr, out int parsedNumber))
            {
                Console.WriteLine($"1. Out Variables: Parsed {parsedNumber} successfully.");
            }

            // 2. Tuples & Deconstruction
            var (name, age) = GetPersonInfo();
            Console.WriteLine($"2. Tuples: Name: {name}, Age: {age}");

            // 3. Pattern Matching (is)
            object obj = "Hello Pattern Matching!";
            if (obj is string strMessage)
            {
                Console.WriteLine($"3. Pattern Matching: {strMessage}");
            }

            // 4. Local Functions
            int AddLocally(int a, int b) => a + b;
            Console.WriteLine($"4. Local Functions: 5 + 10 = {AddLocally(5, 10)}");

            // 5. default literal
            Func<int> myFunc = default;
            Console.WriteLine($"5. Default Literal: myFunc is null? {myFunc == null}");

            // 6. Null-coalescing assignment
            List<int> numbers = null;
            numbers ??= new List<int>();
            numbers.Add(42);
            Console.WriteLine($"6. Null-coalescing Assignment: List count is {numbers.Count}");

            // 7. Switch Expressions
            int status = 1;
            string statusText = status switch
            {
                0 => "Offline",
                1 => "Online",
                _ => "Unknown"
            };
            Console.WriteLine($"7. Switch Expression: Status 1 is '{statusText}'");

            // 8. Async Streams (C# 8.0)
            Console.WriteLine("8. Async Streams: Generating numbers...");
            await foreach (var num in GenerateSequenceAsync())
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine("\n");

            // 9. Using declarations (no brackets needed)
            // Demonstrated in the FileHandling demo, but here is a simple stub
            using var dummyDisposable = new DummyResource();
            Console.WriteLine("9. Using Declarations: Resource created and will dispose at end of scope.\n");
        }

        private static (string, int) GetPersonInfo()
        {
            return ("Alice", 28);
        }

        private static async IAsyncEnumerable<int> GenerateSequenceAsync()
        {
            for (int i = 1; i <= 3; i++)
            {
                await Task.Delay(100); // Simulate async work
                yield return i;
            }
        }
    }

    class DummyResource : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("[DummyResource Disposed]");
        }
    }
}
