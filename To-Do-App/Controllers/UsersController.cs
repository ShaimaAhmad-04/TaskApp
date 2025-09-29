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

        [HttpPut("UpdateUserInfo")]
        public IActionResult UpdateUserInfo([FromBody] UpdateUserDTO updateUserDTO)
        {
            {
                try
                {
                     var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var user = dbContext.Users.FirstOrDefault(x => x.Id == updateUserDTO.Id);

                    if (user == null)
                        return BadRequest(new { message = "User not found" });

                    // check if email is used 
                    if (dbContext.Users.FirstOrDefault(x => x.Email == updateUserDTO.Email) == null)//&& INSTEAD OF CHECKING LIKE THIS, MAKE EMAIL AND USERNAME UNIQUE ATTRIBUTES
                        return BadRequest(new { message = "Email already used " });



                    user.Name = updateUserDTO.Name;
                    user.Email = updateUserDTO.Email;




                    dbContext.SaveChanges();
                    return Ok(new { message = "Changed successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Authorize]
        [HttpPut("forgotPassword")]
        public IActionResult forgotPassword([FromBody] forgotPasswordDTO updateUserDTO)
        {
            try
            {
            //    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = dbContext.Users.FirstOrDefault(x => x.Email == updateUserDTO.Email);

            // check if email is used 
            if (dbContext.Users.FirstOrDefault(x => x.Email == updateUserDTO.Email) == null)//&& INSTEAD OF CHECKING LIKE THIS, MAKE EMAIL AND USERNAME UNIQUE ATTRIBUTES
                    return BadRequest("Email doesn't exists");

               

                user.Email = updateUserDTO.Email;
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.HashedPassword);

                dbContext.SaveChanges();
                return Ok(new { message = "Password changed successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
         [HttpPut("resetPassword")]
        public IActionResult resetPassword([FromBody] resetPasswordDTO updateUserDTO)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);

                if (!BCrypt.Net.BCrypt.Verify(updateUserDTO.currentPassword, user.HashedPassword))
                {
                    return BadRequest(new { message = "Current password is incorrect" });
                }

                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.newPassword);

                dbContext.SaveChanges();
                return Ok(new { message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


