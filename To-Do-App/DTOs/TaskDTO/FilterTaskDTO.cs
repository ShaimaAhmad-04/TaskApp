namespace To_Do_App.DTOs.TaskDTO
{
    public class FilterTaskDTO
    {
        public bool? isComplete { get; set; }
        public long? priorityId { get; set; }
        public DateTime? Deadline { get; set; }


    }
}
