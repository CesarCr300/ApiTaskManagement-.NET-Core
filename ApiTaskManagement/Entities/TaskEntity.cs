namespace ApiTaskManagement.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriorityEntity Priority { get; set; }
        public TaskStateEntity State { get; set; }
        public DateTime? DateClose { get; set; }

        public TaskEntity(string title, string description, TaskPriorityEntity priority, TaskStateEntity state)
        {
            Title = title;
            Description = description;
            Priority = priority;
            State = state;
        }
        public TaskEntity() { }
    }
}
