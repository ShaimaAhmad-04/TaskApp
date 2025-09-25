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
        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            try
            {
                var userId =int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);

                return Ok(user);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateUserInfo")]
        public IActionResult UpdateUserInfo([FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);

        // check if email is used 
                if (dbContext.Users.FirstOrDefault(x => x.Email == updateUserDTO.Email) != null)//&& INSTEAD OF CHECKING LIKE THIS, MAKE EMAIL AND USERNAME UNIQUE ATTRIBUTES
                    return BadRequest("Email already exists");

                user.Email = updateUserDTO.Email;
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.HashedPassword);

                dbContext.SaveChanges();
                return Ok("Profile updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
