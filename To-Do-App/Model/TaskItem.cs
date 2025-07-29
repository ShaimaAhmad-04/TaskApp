using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_App.Model
{
    public class TaskItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? isComplete { get; set; }

        [ForeignKey("UserId")]
        public long? UserId { get; set; }
        public User User { get; set; }
        public long? PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public Lookup? lookup { get; set; }

    }
}
