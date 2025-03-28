using ToDoApp.DTOs.Responses;
using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskResponseDto>> ListAllTasksAsync();
        Task<TaskResponseDto> TaskByIdAsync(Guid id);
        Task<TaskResponseDto> CreateTaskAsync(TaskModel task);
        Task<TaskResponseDto> UpdateTaskAsync(TaskModel task, Guid id);
        Task<bool> RemoveTaskAsync(Guid id);
    }
}
