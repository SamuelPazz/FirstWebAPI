using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoAppDBContext : DbContext
    {
        public ToDoAppDBContext(DbContextOptions<ToDoAppDBContext> options)
            :base(options) 
        { 
        }
        
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
