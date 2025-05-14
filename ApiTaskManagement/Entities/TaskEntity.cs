using ApiTaskManagement.Constants;

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

    public void initialize(string userId)
    {
        UserId = userId;
        if (StateId == TaskStateConstants.Done) DateClose = DateTime.UtcNow;
    }

    public void update(TaskEntity task)
    {
        Title = task.Title;
        Description = task.Description;
        PriorityId = task.PriorityId;

        if (task.StateId != StateId)
        {
            StateId = task.StateId;
            if (StateId == TaskStateConstants.Done) DateClose = DateTime.UtcNow;
            else DateClose = null;
        }

        if (DateClose is not null)
        {
            DateClose = DateClose.Value.ToUniversalTime();
        }
    }
}
