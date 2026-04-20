using Day4PracticeAssignmentApp.Common;
using Day4PracticeAssignmentApp.Scenarios.Banking;
using Day4PracticeAssignmentApp.Scenarios.ECommerce;
using Day4PracticeAssignmentApp.Scenarios.Music;
using Day4PracticeAssignmentApp.Scenarios.OopFeatures;
using Day4PracticeAssignmentApp.Scenarios.SocialMedia;
using Day4PracticeAssignmentApp.Scenarios.Tasks;

List<IScenarioDemo> scenarioDemos = new()
{
    new ECommerceOrderManagementDemo(),
    new SocialMediaEngagementDemo(),
    new BankingTransactionDemo(),
    new MusicPlaylistManagerDemo(),
    new TaskSchedulerDemo(),
    new OopFeaturesDemo()
};

ConsoleOutput.WriteHeading("Day 4 Practice Assignment");

foreach (IScenarioDemo scenarioDemo in scenarioDemos)
{
    ConsoleOutput.WriteHeading(scenarioDemo.Title);
    scenarioDemo.Run();
}

ConsoleOutput.WriteHeading("Practice Assignment Completed");
