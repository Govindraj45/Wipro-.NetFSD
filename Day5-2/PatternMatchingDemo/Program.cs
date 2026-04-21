using System;

namespace PatternMatchingDemo
{
    // Class for Step 4: Property Pattern Matching
    public class Person
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Pattern Matching Scenarios Demo ---\n");

            // Step 1: Take input from the user (simulate "Govindraj" as requested)
            Console.WriteLine("Enter a value (We will use 'Govindraj' automatically as requested):");
            // string inputString = Console.ReadLine();
            string inputString = "Govindraj"; // Using the name you provided
            Console.WriteLine($"Input received: {inputString}\n");

            // We need to parse it to object so we can test different types in the switch
            object parsedInput;
            if (int.TryParse(inputString, out int iVal))
            {
                parsedInput = iVal;
            }
            else if (double.TryParse(inputString, out double dVal))
            {
                parsedInput = dVal;
            }
            else
            {
                parsedInput = inputString;
            }

            // Step 2: Use a switch statement to match the input against different patterns
            Console.WriteLine(">> Step 2: Type Pattern Matching with Switch Statement");
            switch (parsedInput)
            {
                case int i:
                    Console.WriteLine($"The input is an integer: {i}");
                    break;
                case double d:
                    Console.WriteLine($"The input is a double: {d}");
                    break;
                case string s:
                    Console.WriteLine($"The input is a string: {s}");
                    break;
                default:
                    Console.WriteLine("The input is of an unknown type.");
                    break;
            }
            Console.WriteLine();

            // Step 3: Pattern matching with tuples
            Console.WriteLine(">> Step 3: Pattern matching with Tuples");
            var personTuple = (Name: "Govindraj", Age: 30);
            
            switch (personTuple)
            {
                case ("Govindraj", int age):
                    Console.WriteLine($"Match found! The person is Govindraj and he is {age} years old.");
                    break;
                case (string n, int a) when a < 18:
                    Console.WriteLine($"{n} is a minor.");
                    break;
                case (var name, var age):
                    Console.WriteLine($"Deconstructed: Name is {name}, Age is {age}");
                    break;
            }
            Console.WriteLine();

            // Step 4: Pattern matching with properties
            Console.WriteLine(">> Step 4: Pattern matching with Properties");
            var personObj = new Person { Name = "Govindraj", Role = "Developer" };

            // Using property pattern matching
            if (personObj is Person { Name: "Govindraj", Role: var role })
            {
                Console.WriteLine($"Property match successful! The object is a Person named Govindraj, and his role is: {role}");
            }
            else
            {
                Console.WriteLine("Property match failed.");
            }

            Console.WriteLine("\nDemo completed successfully.");
        }
    }
}
