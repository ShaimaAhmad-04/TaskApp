using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs.UserDTO
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
     ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string HashedPassword { get; set; }
    }
}
