using Day4PracticeAssignmentApp.Common;

namespace Day4PracticeAssignmentApp.Scenarios.OopFeatures;

class OopFeaturesDemo : IScenarioDemo
{
    public string Title => "OOP Features: Static, Constructors, Interfaces, Const";

    public void Run()
    {
        IPrintable collectionsModule = new TrainingModule("Collections Practice", 4);
        IPrintable oopModule = new TrainingModule("OOP Features", 2);

        ConsoleOutput.WriteSubHeading("Constructor and Interface Demo");
        collectionsModule.PrintDetails();
        oopModule.PrintDetails();

        ConsoleOutput.WriteSubHeading("Static and Const Demo");
        Console.WriteLine($"Const platform name: {TrainingModule.PlatformName}");
        Console.WriteLine($"Static module count: {TrainingModule.CreatedModuleCount}");
    }
}
