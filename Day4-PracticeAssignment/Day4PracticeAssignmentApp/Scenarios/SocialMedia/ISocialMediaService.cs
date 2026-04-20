namespace Day4PracticeAssignmentApp.Scenarios.SocialMedia;

interface ISocialMediaService
{
    void AddUser(int userId);

    void AddPost(int userId, string post);

    void LikePost(int userId, string post);

    void UndoLastAction();

    void ProcessNotifications();

    void DisplayAnalytics();
}
