namespace Day4PracticeAssignmentApp.Scenarios.SocialMedia;

class SocialMediaService : ISocialMediaService
{
    private readonly List<string> posts = new();
    private readonly Dictionary<string, int> likesPerPost = new();
    private readonly HashSet<int> uniqueUserIds = new();
    private readonly Stack<string> recentActions = new();
    private readonly Queue<string> notifications = new();

    public void AddUser(int userId)
    {
        if (uniqueUserIds.Add(userId))
        {
            recentActions.Push($"Added user {userId}");
            notifications.Enqueue($"Welcome notification sent to user {userId}");
        }
    }

    public void AddPost(int userId, string post)
    {
        if (!uniqueUserIds.Contains(userId))
        {
            Console.WriteLine($"User {userId} is not registered.");
            return;
        }

        posts.Add(post);
        likesPerPost[post] = 0;
        recentActions.Push($"User {userId} added post '{post}'");
        notifications.Enqueue($"New post notification: {post}");
    }

    public void LikePost(int userId, string post)
    {
        if (!uniqueUserIds.Contains(userId))
        {
            Console.WriteLine($"User {userId} is not registered.");
            return;
        }

        if (!likesPerPost.ContainsKey(post))
        {
            Console.WriteLine($"Post '{post}' not found.");
            return;
        }

        likesPerPost[post]++;
        recentActions.Push($"User {userId} liked post '{post}'");
        notifications.Enqueue($"Post '{post}' received a like.");
    }

    public void UndoLastAction()
    {
        if (recentActions.Count == 0)
        {
            Console.WriteLine("No action available to undo.");
            return;
        }

        string action = recentActions.Pop();
        Console.WriteLine($"Undo action: {action}");
    }

    public void ProcessNotifications()
    {
        while (notifications.Count > 0)
        {
            Console.WriteLine($"Processed notification: {notifications.Dequeue()}");
        }
    }

    public void DisplayAnalytics()
    {
        Console.WriteLine($"Unique users: {string.Join(", ", uniqueUserIds)}");
        Console.WriteLine("Posts and likes:");

        foreach (string post in posts)
        {
            Console.WriteLine($"- {post}: {likesPerPost[post]} likes");
        }
    }
}
