using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityBoard.Core.Entities;
using VelocityBoard.Core.Interface;
using VelocityBoard.Infrastructure.Data;

namespace VelocityBoard.Infrastructure.Repostories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly VelocityBoardDbContext _context;
        public TaskRepository(VelocityBoardDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks;
        }

        public async Task<TaskItem> Create(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        
        public async Task<TaskItem> Update(int id, TaskItem task)
        {
            var existing = await _context.Tasks.FindAsync(id);
            if (existing == null) return existing;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
            existing.DueDate = task.DueDate;

            await _context.SaveChangesAsync();
            return existing;
        }

       
        public async Task<string> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return "Not Found";

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return "sucess";
        }
    }
}
