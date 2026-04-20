using System;
using System.Collections.Generic;

static class FeatureTablePrinter
{
    public static void Print()
    {
        ConsoleOutput.WriteHeading("Feature Table");

        List<FeatureRow> features = new()
        {
            new FeatureRow("C# 7", "Tuples", "Return multiple values without creating a separate class."),
            new FeatureRow("C# 7", "Out variables", "Declare out variables directly inside method calls."),
            new FeatureRow("C# 7", "Pattern matching", "Check object shape and type safely using is patterns."),
            new FeatureRow("C# 7", "Local functions", "Keep helper logic close to the method that uses it."),
            new FeatureRow("C# 7", "Deconstruction", "Break a tuple or object into separate variables."),
            new FeatureRow("C# 8", "Switch expressions", "Write compact expression-based selection logic."),
            new FeatureRow("C# 8", "Nullable references", "Identify possible null values earlier."),
            new FeatureRow("C# 8", "Using declarations", "Dispose resources automatically with less nesting."),
            new FeatureRow("C# 8", "Indices and ranges", "Read values from the end or slice collections easily."),
            new FeatureRow("C# 8", "Async streams", "Consume asynchronous data using await foreach."),
            new FeatureRow("Exception handling", "try/catch/finally", "Handle runtime problems without crashing the app."),
            new FeatureRow("File handling", "File and Path", "Create, write, read, and parse files."),
            new FeatureRow("Delegates", "Callback methods", "Pass behavior as a method or lambda."),
            new FeatureRow("Events", "Publisher subscriber", "Notify other classes when an action happens."),
            new FeatureRow("Reflection", "Type inspection", "Read metadata and invoke members at runtime."),
            new FeatureRow("Attributes", "Metadata", "Attach custom information to classes and methods."),
            new FeatureRow("SOLID", "Design principles", "Keep code maintainable, testable, and flexible."),
            new FeatureRow("Design patterns", "Reusable solutions", "Apply proven structures such as Factory and Strategy.")
        };

        Console.WriteLine($"{"Area",-20} {"Feature",-24} Description");
        Console.WriteLine(new string('-', 80));

        foreach (FeatureRow feature in features)
        {
            Console.WriteLine($"{feature.Area,-20} {feature.Name,-24} {feature.Description}");
        }
    }
}

class FeatureRow
{
    public FeatureRow(string area, string name, string description)
    {
        Area = area;
        Name = name;
        Description = description;
    }

    public string Area { get; }
    public string Name { get; }
    public string Description { get; }
}
