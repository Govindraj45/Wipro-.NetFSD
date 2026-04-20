using Day4PracticeAssignmentApp.Common;

namespace Day4PracticeAssignmentApp.Scenarios.Tasks;

class TaskSchedulerDemo : IScenarioDemo
{
    public string Title => "Scenario 5: Task Scheduler System";

    public void Run()
    {
        ITaskSchedulerService taskSchedulerService = new TaskSchedulerService();

        taskSchedulerService.AddTask("Database Backup", 2);
        taskSchedulerService.AddTask("Clear Temp Files", 3);
        taskSchedulerService.AddTask("Security Scan", 1);
        taskSchedulerService.AddTask("Database Backup", 4);

        ConsoleOutput.WriteSubHeading("All Tasks");
        taskSchedulerService.DisplayAllTasks();

        ConsoleOutput.WriteSubHeading("Priority Based Tasks");
        taskSchedulerService.DisplayPriorityTasks();

        ConsoleOutput.WriteSubHeading("Execute Tasks FIFO");
        taskSchedulerService.ExecuteNextTask();
        taskSchedulerService.ExecuteNextTask();

        ConsoleOutput.WriteSubHeading("Undo Last Executed Task");
        taskSchedulerService.UndoLastExecutedTask();
    }
}
