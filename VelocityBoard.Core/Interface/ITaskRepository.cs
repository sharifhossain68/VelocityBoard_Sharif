using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityBoard.Core.Entities;

namespace VelocityBoard.Core.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem> Create(TaskItem task);
        Task<TaskItem> Update(int id, TaskItem task);
        Task<string> Delete(int id);

    }
}
