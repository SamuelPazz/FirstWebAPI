using ToDoApp.Models;

namespace ToDoApp.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>?> FindAllTasksAsync();
        Task<TaskModel?> FindByIdAsync(Guid id);
        Task<TaskModel?> SaveTaskAsync(TaskModel task);
        Task<TaskModel?> UpdateTaskByIdAsync(TaskModel task, Guid id);
        Task<bool> DeleteTaskByIdAsync(Guid id);
    }
}
