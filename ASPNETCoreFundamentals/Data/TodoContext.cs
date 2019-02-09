using ASPNETCoreFundamentals.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        public DbSet<ToDoItem> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Enumerable.Range(1, 10).ToList().ForEach(
                i => modelBuilder.Entity<ToDoItem>().HasData(
                    new ToDoItem
                    {
                        Id = i,
                        IsDone = i % 3 == 0,
                        Name = "Task " + i,
                        Priority = i % 5
                    }
                    ));
        }
    }
}
