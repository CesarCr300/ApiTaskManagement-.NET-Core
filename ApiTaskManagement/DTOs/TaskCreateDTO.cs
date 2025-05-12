namespace ApiTaskManagement.DTOs
{
    public class TaskCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public int StateId { get; set; }
    }
}
