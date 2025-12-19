using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VelocityBoard.API.Service;
using VelocityBoard.Core.Entities;
using VelocityBoard.Core.Interface;

namespace VelocityBoard.API.Controllers
{

    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService  taskService)
        {
            _taskService = taskService; 
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return  Ok( await _taskService.GetAll());
        }
    
        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
           var objtask =  await _taskService.Create(task);
            return Ok(objtask);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem task)
        {
            if (task.Id == 0)
                return BadRequest("ID missing");

            await _taskService.UpdateAsync(id, task);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);
            return Ok();
        }
    }

}
