using Microsoft.EntityFrameworkCore;
using VelocityBoard.Core.Entities;
using VelocityBoard.Infrastructure.Repostories;

namespace VelocityBoard.API.Service
{
    public class TaskService
    {
        readonly private TaskRepository _taskRepository;

         public TaskService( TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            return await _taskRepository.GetAll();
        }

        public async Task<TaskItem> Create(TaskItem task)
        {
          var taskobj =  await _taskRepository.Create(task);
            return taskobj;
        }


        public async Task<TaskItem> Update(int id, TaskItem task)
        {
            var objtask = await _taskRepository.Update(id, task);
            return objtask;
        }


        public async Task<string> Delete(int id)
        {
          return await  _taskRepository.Delete(id);
        }
    }
}
