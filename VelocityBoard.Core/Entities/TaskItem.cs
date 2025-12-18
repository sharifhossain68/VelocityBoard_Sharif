using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityBoard.Core.Entities
{
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}
