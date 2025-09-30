using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs.UserDTO
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string Email { get; set; }

    }
}
