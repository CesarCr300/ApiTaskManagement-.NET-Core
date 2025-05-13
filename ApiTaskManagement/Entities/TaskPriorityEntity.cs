namespace ApiTaskManagement.Entities;
public class TaskPriorityEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}
