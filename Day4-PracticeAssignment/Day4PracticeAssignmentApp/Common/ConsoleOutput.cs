namespace Day4PracticeAssignmentApp.Common;

static class ConsoleOutput
{
    public static void WriteHeading(string heading)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 90));
        Console.WriteLine(heading);
        Console.WriteLine(new string('=', 90));
    }

    public static void WriteSubHeading(string heading)
    {
        Console.WriteLine();
        Console.WriteLine(heading);
        Console.WriteLine(new string('-', heading.Length));
    }
}
