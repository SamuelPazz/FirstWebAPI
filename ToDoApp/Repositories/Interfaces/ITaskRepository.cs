using ToDoApp.Models;

namespace ToDoApp.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>?> FindAllTasksAsync();
        Task<TaskModel?> FindByIdAsync(int id);
        Task<TaskModel?> SaveTaskAsync(TaskModel task);
        Task<TaskModel?> UpdateTaskByIdAsync(TaskModel task, int id);
        Task<bool> DeleteTaskByIdAsync(int id);
    }
}
