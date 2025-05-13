namespace ApiTaskManagement.DTOs;
public class TaskCreateDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PriorityId { get; set; }
    public int StateId { get; set; }
    public DateTime? DateClose { get; set; }
}
