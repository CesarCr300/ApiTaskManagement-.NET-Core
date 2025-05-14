namespace ApiTaskManagement.DTOs
{
    public class TaskUpdateDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PriorityId { get; set; }
        public int StateId { get; set; }
    }

}
