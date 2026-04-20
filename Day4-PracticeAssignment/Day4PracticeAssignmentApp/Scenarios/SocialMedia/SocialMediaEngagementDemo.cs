using Day4PracticeAssignmentApp.Common;

namespace Day4PracticeAssignmentApp.Scenarios.SocialMedia;

class SocialMediaEngagementDemo : IScenarioDemo
{
    public string Title => "Scenario 2: Social Media Platform User Engagement System";

    public void Run()
    {
        ISocialMediaService socialMediaService = new SocialMediaService();

        socialMediaService.AddUser(501);
        socialMediaService.AddUser(502);
        socialMediaService.AddUser(501);

        socialMediaService.AddPost(501, "Learning C# collections");
        socialMediaService.AddPost(502, "Building backend systems");
        socialMediaService.LikePost(502, "Learning C# collections");
        socialMediaService.LikePost(501, "Building backend systems");
        socialMediaService.LikePost(502, "Building backend systems");

        ConsoleOutput.WriteSubHeading("Engagement Analytics");
        socialMediaService.DisplayAnalytics();

        ConsoleOutput.WriteSubHeading("Undo Feature");
        socialMediaService.UndoLastAction();

        ConsoleOutput.WriteSubHeading("Process Notifications FIFO");
        socialMediaService.ProcessNotifications();
    }
}
