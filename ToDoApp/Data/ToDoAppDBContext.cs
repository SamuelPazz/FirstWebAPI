using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Map;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoAppDBContext : DbContext
    {
        public ToDoAppDBContext(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ToDoAppDBContext).Assembly);
        }
    }
}
