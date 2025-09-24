using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs.UserDTO
{
    public class UserLogInDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
