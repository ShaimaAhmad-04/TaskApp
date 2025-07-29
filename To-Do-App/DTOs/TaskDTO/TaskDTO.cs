using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs.TaskDTO
{
    public class TaskDTO
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? isComplete { get; set; }
        public long? priorityId { get; set; }
        public string? priorityName { get; set; }
        public string UserName { get; set; }

        public long UserID { get; set; }
    }
}
