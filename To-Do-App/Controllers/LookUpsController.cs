using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_App.DTOs.lookUpDTO;

namespace To_Do_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpsController : ControllerBase
    {
        private ToDoApp_DbContext dbContext;
      public  LookUpsController(  ToDoApp_DbContext _DbContext)
        { 
            dbContext = _DbContext;
        }


        [HttpGet("getPriorities")]
        public IActionResult getPriorities([FromQuery]int MajorCode)
        {
            try
            {
                var data = from lookUp in dbContext.Lookups
                           where lookUp.MajorCode == MajorCode && MajorCode == 0 && lookUp.MinorCode != 0 
                           select new listDTO
                           {
                               Id = lookUp.Id,
                               Name = lookUp.Name,
                           };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
