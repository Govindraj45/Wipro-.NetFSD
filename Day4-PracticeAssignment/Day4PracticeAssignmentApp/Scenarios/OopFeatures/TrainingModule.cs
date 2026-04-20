namespace Day4PracticeAssignmentApp.Scenarios.OopFeatures;

class TrainingModule : IPrintable
{
    public const string PlatformName = "Wipro .NetFSD";
    private static int createdModuleCount;

    public TrainingModule(string moduleName, int durationInHours)
    {
        ModuleName = moduleName;
        DurationInHours = durationInHours;
        createdModuleCount++;
    }

    public string ModuleName { get; }
    public int DurationInHours { get; }

    public static int CreatedModuleCount => createdModuleCount;

    public void PrintDetails()
    {
        Console.WriteLine($"Platform: {PlatformName}, Module: {ModuleName}, Duration: {DurationInHours} hours");
    }
}
