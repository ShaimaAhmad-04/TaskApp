using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_App.DTOs.UserDTO;

namespace To_Do_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ToDoApp_DbContext dbContext;

        public UsersController(ToDoApp_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize]
        [HttpPut("UpdateUserInfo")]
        public IActionResult UpdateUserInfo([FromBody]UpdateUserDTO updateUserDTO)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
                return NotFound("User not found");

            if (!(string.IsNullOrEmpty(updateUserDTO.Email)) &&
                     !dbContext.Users.Any(x => x.Email == updateUserDTO.Email &&
                      x.Id != userId))

                user.Email = updateUserDTO.Email;

            if (!(string.IsNullOrWhiteSpace(updateUserDTO.Name)))
                user.Name = updateUserDTO.Name;

            if (!(string.IsNullOrEmpty(updateUserDTO.HashedPassword)))
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.HashedPassword);

            dbContext.SaveChanges();
            return Ok("Profile updated successfully");
        }
    }
}
