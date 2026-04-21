public delegate int MathOperation(int a, int b);

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
        Console.WriteLine($"Multiply Result: {multiplyResult}");
    }
}
