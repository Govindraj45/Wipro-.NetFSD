using System;
using System.IO;

namespace Day5AdvancedCSharpDemo
{
    public class CustomDemoException : Exception
    {
        public CustomDemoException(string message) : base(message) { }
    }

    public class ExceptionFileHandlingDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("--- Exception & File Handling Demo ---\n");
            
            string filePath = "demo_file.txt";

            // 1. File Writing and Custom Exception
            try
            {
                Console.WriteLine("Attempting to write to file...");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Hello from C# File Handling!");
                    writer.WriteLine("This is the second line.");
                }
                Console.WriteLine($"Successfully wrote to {filePath}");

                // Simulate throwing a custom exception
                Console.WriteLine("Simulating an error condition...");
                throw new CustomDemoException("This is a simulated custom exception.");
            }
            catch (CustomDemoException ex)
            {
                Console.WriteLine($"[Caught Custom Exception]: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[Caught IO Exception]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Caught General Exception]: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Finally block executed after writing attempt.\n");
            }

            // 2. File Reading
            try
            {
                Console.WriteLine("Attempting to read from file...");
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string content = reader.ReadToEnd();
                        Console.WriteLine("File Content:");
                        Console.WriteLine(content);
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}
