using System;
using System.Reflection;

static class ReflectionAttributeDemo
{
    public static void Run()
    {
        ConsoleOutput.WriteHeading("Reflection and Attributes");

        Type reportType = typeof(OrderReport);
        Console.WriteLine($"Class discovered using reflection: {reportType.Name}");

        DemoAttribute? classAttribute = reportType.GetCustomAttribute<DemoAttribute>();
        if (classAttribute is not null)
        {
            Console.WriteLine($"Class attribute: {classAttribute.Title} - {classAttribute.Description}");
        }

        object? reportInstance = Activator.CreateInstance(reportType);
        MethodInfo[] methods = reportType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        foreach (MethodInfo method in methods)
        {
            DemoAttribute? methodAttribute = method.GetCustomAttribute<DemoAttribute>();
            if (methodAttribute is null)
            {
                continue;
            }

            Console.WriteLine($"Method attribute: {method.Name} - {methodAttribute.Title}");
            method.Invoke(reportInstance, null);
        }
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
class DemoAttribute : Attribute
{
    public DemoAttribute(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }
}

[Demo("Order report class", "Shows how custom attributes can describe a class.")]
class OrderReport
{
    [Demo("Generate report", "This method is discovered and called using reflection.")]
    public void Generate()
    {
        Console.WriteLine("Order report generated using reflection.");
    }
}
