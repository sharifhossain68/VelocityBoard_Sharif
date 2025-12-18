using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VelocityBoard.API.Service;
using VelocityBoard.Core.Entities;
using VelocityBoard.Core.Interface;

namespace VelocityBoard.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class TaskController : ControllerBase
    //{
    //}


    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService  taskService)
        {
            _taskService = taskService; ;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return  Ok(_taskService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
           var objtask =  await _taskService.Create(task);
            return Ok(objtask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem task)
        {
            var existing = await _taskService.Update(id,task);
           
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);
            return Ok();
        }
    }

}
