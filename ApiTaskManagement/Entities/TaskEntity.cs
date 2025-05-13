namespace ApiTaskManagement.Entities;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PriorityId { get; set; }
    public int StateId { get; set; }
    public DateTime? DateClose { get; set; }
    public string UserId { get; set; } =null!;

    public TaskPriorityEntity Priority { get; set; } = null!;
    public TaskStateEntity State { get; set; } = null!;
}
