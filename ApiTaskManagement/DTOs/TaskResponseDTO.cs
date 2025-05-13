namespace ApiTaskManagement.DTOs
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PriorityName { get; set; } = null!;
        public int PriorityId { get; set; }
        public string StateName { get; set; } = null!;
        public int StateId { get; set; }
        public DateTime? DateClose { get; set; }
    }

}
