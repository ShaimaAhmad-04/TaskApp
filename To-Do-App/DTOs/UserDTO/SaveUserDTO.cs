using System.ComponentModel.DataAnnotations;
using To_Do_App.Model;

namespace To_Do_App.DTOs.UserDTO
{
    public class SaveUserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public string? Bio {  get; set; }   
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
