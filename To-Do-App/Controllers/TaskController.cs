using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_App.DTOs.TaskDTO;
namespace To_Do_App.Model;

     [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ToDoApp_DbContext _DbContext;

        public TaskController(ToDoApp_DbContext dbContext)
        {
            _DbContext = dbContext;
        }
    // get all tasks based on a filter 
        [HttpGet("GetALL")]
        public IActionResult GetAll([FromQuery]FilterTaskDTO? taskDTO)
        {
          try {   
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); //logged in userID

             var task = from taskItem in _DbContext.Tasks
                    from priority in _DbContext.Lookups.Where(x=> x.Id == taskItem.PriorityId).DefaultIfEmpty()
                    where
                        (taskItem.UserId == UserId) &&
                        (taskDTO.isComplete == null || taskDTO.isComplete == taskItem.isComplete) &&
                        (taskDTO.Deadline == null || taskDTO.Deadline == taskItem.Deadline) &&
                        (taskDTO.priorityId == null || taskDTO.priorityId == taskItem.PriorityId)               
                select new TaskDTO
                {
                    Id = taskItem.Id,
                    Name = taskItem.Name,
                    Description = taskItem.Description,
                    priorityName = priority.Name,
                    priorityId = taskItem.PriorityId,
                    isComplete = taskItem.isComplete,
                    UserID = (long)taskItem.UserId,
                    UserName = taskItem.User.Name  ,
                    Deadline = taskItem.Deadline,
               };
            

            if (task == null)
                return BadRequest("No tasks exist");

            return Ok(task);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    // get all tasks for a user 
    [HttpGet("GetId")]
    public IActionResult GetId([FromQuery] long? Id)
    {
      try {
        var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var task = from taskItem in _DbContext.Tasks
                   where (taskItem.UserId == UserId) && (Id == null ||  taskItem.Id == Id) 
                   select new TaskDTO
                   {
                       Id = taskItem.Id,
                       Name = taskItem.Name,
                       Description = taskItem.Description,
                       priorityName = taskItem.lookup.Name,
                       priorityId = taskItem.PriorityId,
                       isComplete = taskItem.isComplete,
                       UserID = (long)taskItem.UserId,
                       UserName = taskItem.User.Name,
                       Deadline = taskItem.Deadline,
                   };

            if (task == null)
                return BadRequest("No tasks exist");

            return Ok(task); 
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
        [HttpPost("Add")]
        public IActionResult Add([FromBody]SaveTaskDTO addTaskDTO)
        {
          try {  
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var newTask = new TaskItem
                {
                    Id = 0,
                    Name = addTaskDTO.Name,
                    Description = addTaskDTO.Description,
                    PriorityId = addTaskDTO.priorityId,
                    isComplete = addTaskDTO.isComplete,
                    Deadline = addTaskDTO.Deadline,
                    UserId = UserID
                };

                _DbContext.Tasks.Add(newTask);
                 _DbContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPut("Update")]
    public IActionResult Update([FromBody]SaveTaskDTO taskDTO)
    {
      try {    
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var newTask = _DbContext.Tasks.FirstOrDefault(task => task.Id == taskDTO.Id && task.UserId == UserID) ;

            if (newTask == null)
                return BadRequest("no taske found");
        
            newTask.Name = taskDTO.Name;
            newTask.isComplete = taskDTO.isComplete;
            newTask.Deadline = taskDTO.Deadline;
            newTask.Description = taskDTO.Description;
            newTask.PriorityId = taskDTO.priorityId;

            _DbContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("Delete")]
     public IActionResult Delete([FromQuery]long ? Id)
    {
       try {
            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var task = _DbContext.Tasks.FirstOrDefault(x => x.Id == Id && x.UserId == UserID);

            if (task == null)
                return BadRequest();

            _DbContext.Tasks.Remove(task);
            _DbContext.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
   }
