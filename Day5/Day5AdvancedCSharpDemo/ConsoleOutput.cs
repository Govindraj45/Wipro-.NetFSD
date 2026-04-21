using System;

static class ConsoleOutput
{
    public static void WriteHeading(string heading)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 80));
        Console.WriteLine(heading);
        Console.WriteLine(new string('=', 80));
    }

    public static void WriteSubHeading(string heading)
    {
        Console.WriteLine();
        Console.WriteLine(heading);
        Console.WriteLine(new string('-', heading.Length));
    }
}
