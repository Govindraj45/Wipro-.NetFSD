using TaskManagementSecurityApp.Models;

namespace TaskManagementSecurityApp.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks =
    [
        new TaskItem { Id = 1, Title = "Review sprint board", Description = "Check blockers and update status.", OwnerUserName = "member" },
        new TaskItem { Id = 2, Title = "Audit overdue tasks", Description = "Admin-only oversight for stalled work items.", OwnerUserName = "admin" }
    ];

    private int _nextId = 3;

    public IReadOnlyList<TaskItem> GetUserTasks(string userName) =>
        _tasks.Where(task => task.OwnerUserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).ToList();

    public IReadOnlyList<TaskItem> GetAllTasks() => _tasks.OrderBy(task => task.OwnerUserName).ThenBy(task => task.Title).ToList();

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(task => task.Id == id);

    public void Add(TaskItem task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
    }

    public bool Update(TaskItem task, string actor)
    {
        var existing = GetById(task.Id);
        if (existing is null || !existing.OwnerUserName.Equals(actor, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        existing.Title = task.Title;
        existing.Description = task.Description;
        return true;
    }
}
