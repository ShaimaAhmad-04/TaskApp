using System.ComponentModel.DataAnnotations;

namespace To_Do_App.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public List<TaskItem>? taskItems { get; set; }

    }
}
