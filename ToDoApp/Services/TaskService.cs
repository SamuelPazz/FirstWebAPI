using ToDoApp.DTOs.Responses;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Status = t.Status,
                UserId = t.UserId,
                User = t.User != null ? new UserResponseDto
                {
                    Id = t.User.Id,
                    Name = t.User.Name,
                    Email = t.User.Email
                } : null
            }).ToList();
        }

        public async Task<TaskResponseDto> TaskByIdAsync(Guid id)
        {
            TaskModel? task = await _taskRepository.FindByIdAsync(id);

            if (task == null)
            {
                _logger.LogError($"Task with id {id} not found.");
                throw new NotFoundException($"Task with id {id} not found.");
            }

            return new TaskResponseDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                UserId = task.UserId,
                User = task.User != null ? new UserResponseDto
                {
                    Id = task.User.Id,
                    Name = task.User.Name,
                    Email = task.User.Email
                } : null
            };
        }

        public async Task<TaskResponseDto> CreateTaskAsync(TaskModel task)
        {
            TaskModel? createdTask = await _taskRepository.SaveTaskAsync(task);

            if (createdTask == null)
            {
                _logger.LogError("Task creation failed.");
                throw new BadRequestException("Task creation failed.");
            }

            return new TaskResponseDto
            {
                Id = createdTask.Id,
                Name = createdTask.Name,
                Description = createdTask.Description,
                Status = createdTask.Status,
                UserId = createdTask.UserId,
                User = createdTask.User != null ? new UserResponseDto
                {
                    Id = createdTask.User.Id,
                    Name = createdTask.User.Name,
                    Email = createdTask.User.Email
                } : null
            };
        }

        public async Task<TaskResponseDto> UpdateTaskAsync(TaskModel task, Guid id)
        {
            task.Id = id; 
            TaskModel? updatedTask = await _taskRepository.UpdateTaskByIdAsync(task, id);

            if (updatedTask == null)
            {
                _logger.LogError($"Task with id {id} not found for update.");
                throw new NotFoundException($"Task with id {id} not found for update.");
            }

            return new TaskResponseDto
            {
                Id = updatedTask.Id,
                Name = updatedTask.Name,
                Description = updatedTask.Description,
                Status = updatedTask.Status,
                UserId = updatedTask.UserId,
                User = updatedTask.User != null ? new UserResponseDto
                {
                    Id = updatedTask.User.Id,
                    Name = updatedTask.User.Name,
                    Email = updatedTask.User.Email
                } : null
            };
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
