using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do_App.DTOs.TaskDTO;
using To_Do_App.DTOs.UserDTO;
using To_Do_App.Model;

namespace To_Do_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ToDoApp_DbContext _DbContext;

        public AuthController(ToDoApp_DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp([FromBody] SaveUserDTO saveUserDTO)
        {
            try
            {
                var userCheck = _DbContext.Users.FirstOrDefault(x => x.Email == saveUserDTO.Email);

                // email not used 
                 if (userCheck == null)
                {
                    var user = new User
                    {
                        Id = 0,
                        Email = saveUserDTO.Email,
                        Name = saveUserDTO.Name,
                        HashedPassword = BCrypt.Net.BCrypt.HashPassword($"{saveUserDTO.Name}@123"),
                        IsAdmin = false
                    };


                    _DbContext.Users.Add(user);
                    _DbContext.SaveChanges();


                    return Ok();
                }
                 //email used 
                else
                {
                    return BadRequest("Email already used");
                }

            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn([FromBody] UserLogInDTO userLogInDTO)
        {
            try 
          {  var userCheck = _DbContext.Users.FirstOrDefault(x => x.Email == userLogInDTO.Email);

            if (userCheck == null)
                return BadRequest("Invalid username or password");

            if (!BCrypt.Net.BCrypt.Verify(userLogInDTO.Password, userCheck.HashedPassword))
                return BadRequest("Invalid username or password");
                // Check if user has any tasks
                var hasTasks = _DbContext.Tasks.Any(t => t.UserId == userCheck.Id);

                // If no tasks, create a special "Welcome" task
                if (!hasTasks)
                {
                    var welcomeTask = new TaskItem
                    {
                        Name = "Start by adding your first task",
                        //Description = $"Hello {userCheck.Name}, this is your first task. Let's get started!",
                        //PriorityId = null, // or default
                        //Deadline = null,
                        //isComplete = false,
                        UserId = userCheck.Id,
                    };
                    _DbContext.Tasks.Add(welcomeTask);
                    _DbContext.SaveChanges();
                }

                var token = GenerateToken(userCheck);
                return Ok(new
                {
                    token = token,
                    userId = userCheck.Id
                });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {

            new Claim (ClaimTypes.NameIdentifier , user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            else
                claims.Add(new Claim(ClaimTypes.Role, "User"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!WlokwHAGTI9D5$$LOE%msSH##ADG**JA"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var settings = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(settings);

            return token;

        }
    }
}
