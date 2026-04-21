using System;

// Step 1: Define a delegate type
public delegate int MathOperation(int a, int b);

// Step 2: Create a class that will act as the publisher
public class MathPublisher
{
    // Define an event that uses the delegate type
    public event MathOperation OnMathOperationPerformed;

    public void PerformOperation(int a, int b)
    {
        Console.WriteLine($"\n[Publisher] Publishing math operation for numbers: {a} and {b}");
        // Step 5: Invoke the publisher's event to notify all subscribers
        if (OnMathOperationPerformed != null)
        {
            // Note: Since MathOperation returns an int, invoking a multicast delegate
            // will only return the value of the last method called in the invocation list.
            int finalResult = OnMathOperationPerformed.Invoke(a, b);
            Console.WriteLine($"[Publisher] The last subscriber returned: {finalResult}");
        }
        else
        {
            Console.WriteLine("[Publisher] No subscribers found.");
        }
    }
}

// Step 3: Create a class that will act as the subscriber
public class MathSubscriber
{
    public string Name { get; set; }

    public MathSubscriber(string name)
    {
        Name = name;
    }

    // Define a method that matches the signature of the delegate
    public int HandleMathOperation(int a, int b)
    {
        int sum = a + b;
        Console.WriteLine($"[Subscriber {Name}] Handled math operation: {a} + {b} = {sum}");
        return sum; // returns an integer as output
    }
}

class Program
{
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Subtract(int a, int b)
    {
        return a - b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    static void Main(string[] args)
    {
        int num1 = 25;
        int num2 = 10;
        
        Console.WriteLine($"Testing MathOperation Delegate with numbers: {num1} and {num2}\n");

        MathOperation operation = Add;
        int addResult = operation(num1, num2);
        Console.WriteLine($"Add Result: {addResult}");

        operation = Subtract;
        int subtractResult = operation(num1, num2);
        Console.WriteLine($"Subtract Result: {subtractResult}");

        operation = Multiply;
        int multiplyResult = operation(num1, num2);
        Console.WriteLine($"Multiply Result: {multiplyResult}\n");

        Console.WriteLine("--- Publish-Subscribe Model Demo ---");

        // Step 4: Create an instance of the publisher
        MathPublisher publisher = new MathPublisher();

        // Create instances of subscribers
        MathSubscriber sub1 = new MathSubscriber("Alice");
        MathSubscriber sub2 = new MathSubscriber("Bob");

        // Subscribe the subscriber's method to the publisher's event
        publisher.OnMathOperationPerformed += sub1.HandleMathOperation;
        publisher.OnMathOperationPerformed += sub2.HandleMathOperation;

        // Step 5: Invoke the publisher's event
        publisher.PerformOperation(10, 5);
        
        // Unsubscribe Alice and perform again
        Console.WriteLine("\n[System] Alice unsubscribes...");
        publisher.OnMathOperationPerformed -= sub1.HandleMathOperation;
        publisher.PerformOperation(8, 4);
    }
}
