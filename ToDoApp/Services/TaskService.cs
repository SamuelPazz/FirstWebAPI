using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models.DTOs.Responses;
using ToDoApp.Models.Mappers;
using ToDoApp.Models.DTOs.Requests;

namespace ToDoApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<List<TaskResponseDto>> ListAllTasksAsync()
        {
            List<TaskModel>? tasks = await _taskRepository.FindAllTasksAsync();

            if (tasks == null || !tasks.Any())
            {
                _logger.LogError("No tasks found.");
                return new List<TaskResponseDto>();
            }

            return tasks.Select(TaskMapper.Of).ToList();
        }

        public async Task<TaskResponseDto> TaskByIdAsync(Guid id)
        {
            TaskModel? task = await _taskRepository.FindByIdAsync(id);

            if (task == null)
            {
                _logger.LogError($"Task with id {id} not found.");
                throw new NotFoundException($"Task with id {id} not found.");
            }

            return TaskMapper.Of(task);
        }

        public async Task<TaskResponseDto> CreateTaskAsync(TaskCreateDto taskCreateDto)
        {
            TaskModel? createdTask = await _taskRepository.SaveTaskAsync(TaskMapper.Of(taskCreateDto));

            if (createdTask == null)
            {
                _logger.LogError("Task creation failed.");
                throw new BadRequestException("Task creation failed.");
            }

            return TaskMapper.Of(createdTask);
        }

        public async Task<TaskResponseDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto, Guid taskId)
        {
            TaskModel? updatedTask = await _taskRepository.UpdateTaskByIdAsync(TaskMapper.Of(taskUpdateDto), taskId);

            if (updatedTask == null)
            {
                _logger.LogError($"Task with id {taskId} or User with id {taskUpdateDto.UserId} not found for update task");
                throw new NotFoundException($"Task with id {taskId} or User with id {taskUpdateDto.UserId} not found for update task");
            }

            return TaskMapper.Of(updatedTask);
        }

        public async Task<bool> RemoveTaskAsync(Guid id)
        {
            bool result = await _taskRepository.DeleteTaskByIdAsync(id);

            if (!result)
            {
                _logger.LogError($"Task with id {id} not found for deletion.");
                throw new NotFoundException($"Task with id {id} not found for deletion.");
            }

            return result;
        }
    }
}
