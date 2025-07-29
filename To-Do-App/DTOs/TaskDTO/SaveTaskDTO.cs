namespace To_Do_App.DTOs.TaskDTO
{
    public class SaveTaskDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? isComplete { get; set; }
        public long? priorityId { get; set; }
    }
}
