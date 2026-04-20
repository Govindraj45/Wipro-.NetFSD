static class PatternMatchingDemo
{
    public static void Run()
    {
        Console.WriteLine("Enter a value:");
        string input = Console.ReadLine() ?? "Govindraj";

        if (string.IsNullOrWhiteSpace(input))
        {
            input = "Govindraj";
        }

        object convertedInput = ConvertInput(input);

        Console.WriteLine();
        Console.WriteLine("Step 1 and 2: Type pattern matching with switch statement");
        MatchInputType(convertedInput);

        Console.WriteLine();
        Console.WriteLine("Step 3: Tuple pattern matching");
        MatchTuple((convertedInput, input.Length));

        Console.WriteLine();
        Console.WriteLine("Step 4: Property pattern matching");
        UserProfile profile = new(input, "Student", input.Length);
        MatchUserProfile(profile);
    }

    private static object ConvertInput(string input)
    {
        if (int.TryParse(input, out int integerValue))
        {
            return integerValue;
        }

        if (double.TryParse(input, out double doubleValue))
        {
            return doubleValue;
        }

        return input;
    }

    private static void MatchInputType(object input)
    {
        switch (input)
        {
            case int integerValue:
                Console.WriteLine($"The input is an integer: {integerValue}");
                break;

            case double doubleValue:
                Console.WriteLine($"The input is a double: {doubleValue}");
                break;

            case string stringValue:
                Console.WriteLine($"The input is a string: {stringValue}");
                break;

            default:
                Console.WriteLine("The input is of an unknown type.");
                break;
        }
    }

    private static void MatchTuple((object value, int length) inputDetails)
    {
        switch (inputDetails)
        {
            case (string name, >= 8):
                Console.WriteLine($"Tuple matched a long string name: {name}");
                break;

            case (string name, > 0):
                Console.WriteLine($"Tuple matched a short string name: {name}");
                break;

            case (int number, _):
                Console.WriteLine($"Tuple matched an integer value: {number}");
                break;

            case (double number, _):
                Console.WriteLine($"Tuple matched a double value: {number}");
                break;

            default:
                Console.WriteLine("Tuple did not match a known pattern.");
                break;
        }
    }

    private static void MatchUserProfile(UserProfile profile)
    {
        switch (profile)
        {
            case { Name: "Govindraj", Role: "Student", NameLength: >= 8 }:
                Console.WriteLine("Property pattern matched: Govindraj is a student and the name length is 8 or more.");
                break;

            case { Role: "Student" }:
                Console.WriteLine($"Property pattern matched: {profile.Name} is a student.");
                break;

            case { NameLength: 0 }:
                Console.WriteLine("Property pattern matched: name is empty.");
                break;

            default:
                Console.WriteLine("Property pattern did not match a known user profile.");
                break;
        }
    }
}
