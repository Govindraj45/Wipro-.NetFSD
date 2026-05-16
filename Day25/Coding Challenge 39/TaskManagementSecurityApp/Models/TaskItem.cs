using System.ComponentModel.DataAnnotations;

namespace TaskManagementSecurityApp.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Title { get; set; } = string.Empty;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public string OwnerUserName { get; set; } = string.Empty;
}
