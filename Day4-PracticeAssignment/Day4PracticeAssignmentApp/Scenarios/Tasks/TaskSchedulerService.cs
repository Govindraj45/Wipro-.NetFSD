namespace Day4PracticeAssignmentApp.Scenarios.Tasks;

class TaskSchedulerService : ITaskSchedulerService
{
    private readonly Queue<string> executionQueue = new();
    private readonly Stack<string> undoStack = new();
    private readonly List<string> allTasks = new();
    private readonly SortedDictionary<int, string> priorityTasks = new();
    private readonly HashSet<string> uniqueTasks = new(StringComparer.OrdinalIgnoreCase);

    public void AddTask(string taskName, int priority)
    {
        if (!uniqueTasks.Add(taskName))
        {
            Console.WriteLine($"Duplicate task skipped: {taskName}");
            return;
        }

        allTasks.Add(taskName);
        executionQueue.Enqueue(taskName);
        priorityTasks[priority] = taskName;
    }

    public void ExecuteNextTask()
    {
        if (executionQueue.Count == 0)
        {
            Console.WriteLine("No tasks available for execution.");
            return;
        }

        string taskName = executionQueue.Dequeue();
        undoStack.Push(taskName);
        Console.WriteLine($"Executed task: {taskName}");
    }

    public void UndoLastExecutedTask()
    {
        if (undoStack.Count == 0)
        {
            Console.WriteLine("No executed task available to undo.");
            return;
        }

        string taskName = undoStack.Pop();
        executionQueue.Enqueue(taskName);
        Console.WriteLine($"Undo completed. Task added back to queue: {taskName}");
    }

    public void DisplayAllTasks()
    {
        Console.WriteLine("All tasks: " + string.Join(", ", allTasks));
    }

    public void DisplayPriorityTasks()
    {
        foreach (KeyValuePair<int, string> task in priorityTasks)
        {
            Console.WriteLine($"Priority {task.Key}: {task.Value}");
        }
    }
}
