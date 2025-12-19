using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityBoard.Core.Entities;

namespace VelocityBoard.Infrastructure.Data
{
    public class VelocityBoardDbContext : DbContext
    {
        public VelocityBoardDbContext(DbContextOptions<VelocityBoardDbContext> options)
       : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Set primary key 
            modelBuilder.Entity<User>()
                .HasKey(U => U.Id);
            modelBuilder.Entity<TaskItem>()
              .HasKey(T => T.Id);


        }
    }
}
