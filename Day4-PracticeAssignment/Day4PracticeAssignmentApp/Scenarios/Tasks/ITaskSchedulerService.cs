namespace Day4PracticeAssignmentApp.Scenarios.Tasks;

interface ITaskSchedulerService
{
    void AddTask(string taskName, int priority);

    void ExecuteNextTask();

    void UndoLastExecutedTask();

    void DisplayAllTasks();

    void DisplayPriorityTasks();
}
