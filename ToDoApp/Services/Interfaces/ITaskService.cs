using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Models.DTOs.Responses;

namespace ToDoApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskResponseDto>> ListAllTasksAsync();
        Task<TaskResponseDto> TaskByIdAsync(Guid id);
        Task<TaskResponseDto> CreateTaskAsync(TaskCreateDto taskCreateDto);
        Task<TaskResponseDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto, Guid taskId);
        Task<bool> RemoveTaskAsync(Guid id);
    }
}
