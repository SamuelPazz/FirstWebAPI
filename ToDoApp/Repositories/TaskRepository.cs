using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoAppDBContext _dbContext;

        public TaskRepository(ToDoAppDBContext toDoAppDBContext)
        {
            _dbContext = toDoAppDBContext;
        }

        public async Task<TaskModel?> FindByIdAsync(int id)
        {
            var result =  await _dbContext.Tasks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return null;

            return result;
        }

        public async Task<List<TaskModel>?> FindAllTasksAsync()
        {
            var result = await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();

            if (!result.Any()) 
                return null;

            return result;
        }

        public async Task<TaskModel?> SaveTaskAsync(TaskModel task)
        {
            if (task == null) 
                return null;

            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel?> UpdateTaskByIdAsync(TaskModel task, int id)
        {
            TaskModel? taskById = await FindByIdAsync(id);

            if (taskById == null)
                return null;

            taskById.Name = task.Name;
            taskById.Status = task.Status;
            taskById.Description = task.Description;
            taskById.UserId = task.UserId;

            _dbContext.Tasks.Update(taskById);
            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> DeleteTaskByIdAsync(int id)
        {
            TaskModel? taskById = await FindByIdAsync(id);

            if (taskById == null)
                return false;

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
