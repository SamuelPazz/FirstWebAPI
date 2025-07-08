using Microsoft.Extensions.Logging;
using Moq;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services;
using ToDoApp.Services.Interfaces;

namespace ToDoAppTest
{
    public class TaskTests
    {
        private readonly Mock<ITaskRepository> _taskRepository;
        private readonly Mock<ILogger<TaskService>> _logger;
        private readonly ITaskService _taskService;

        public TaskTests()
        {
            _taskRepository = new Mock<ITaskRepository>();
            _logger = new Mock<ILogger<TaskService>>();
            _taskService = new TaskService(_taskRepository.Object, _logger.Object);
        }

        [Fact]
        public async Task ListAllTasksAsync_ReturnsListOfTaskResponseDto_WhenTasksExist()
        {
            // Arrange
            var tasks = new List<TaskModel>
            {
                new() { Id = Guid.NewGuid(), Name = "Task 1", Description = "Description 1", Status = "Pending", User = new UserModel() },
                new() { Id = Guid.NewGuid(), Name = "Task 2", Description = "Description 2", Status = "Finished", User = new UserModel() }
            };
            _taskRepository.Setup(r => r.FindAllTasksAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _taskService.ListAllTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListAllTasksAsync_ReturnsEmptyList_WhenNoTasksExist()
        {
            // Arrange
            _taskRepository.Setup(r => r.FindAllTasksAsync()).ReturnsAsync(new List<TaskModel>());

            // Act
            var result = await _taskService.ListAllTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TaskByIdAsync_ReturnsTask_WhenFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new TaskModel { Id = taskId, Name = "Task", User = new UserModel() };
            _taskRepository.Setup(r => r.FindByIdAsync(taskId)).ReturnsAsync(task);

            // Act
            var result = await _taskService.TaskByIdAsync(taskId);

            // Assert
            Assert.Equal(taskId, result.Id);
            Assert.Equal("Task", result.Name);
        }

        [Fact]
        public async Task TaskByIdAsync_ThrowsNotFound_WhenNotExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _taskRepository.Setup(r => r.FindByIdAsync(taskId)).ReturnsAsync((TaskModel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _taskService.TaskByIdAsync(taskId));
        }

        [Fact]
        public async Task CreateTaskAsync_ReturnsTaskResponse_WhenCreated()
        {
            // Arrange
            var dto = new TaskCreateDto { Name = "New Task", Description = "Description" };
            var createdTask = new TaskModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                User = new UserModel()
            };
            _taskRepository.Setup(r => r.SaveTaskAsync(It.IsAny<TaskModel>())).ReturnsAsync(createdTask);

            // Act
            var result = await _taskService.CreateTaskAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Task", result.Name);
        }

        [Fact]
        public async Task CreateTaskAsync_ThrowsBadRequest_WhenFailsToCreate()
        {
            // Arrange
            var dto = new TaskCreateDto { Name = "Error", Description = "Error"};
            _taskRepository.Setup(r => r.SaveTaskAsync(It.IsAny<TaskModel>())).ReturnsAsync((TaskModel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _taskService.CreateTaskAsync(dto));
        }

        [Fact]
        public async Task UpdateTaskAsync_ReturnsTaskResponse_WhenUpdated()
        {
            // Arrange
            var dto = new TaskUpdateDto
            {
                Name = "Updated",
                Description = "New description",
                Status = "Finished",
                UserId = Guid.NewGuid()
            };
            var updated = new TaskModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status,
                User = new UserModel { Id = (Guid)dto.UserId }
            };
            _taskRepository.Setup(r => r.UpdateTaskByIdAsync(It.IsAny<TaskModel>(), updated.Id)).ReturnsAsync(updated);

            // Act
            var result = await _taskService.UpdateTaskAsync(dto, updated.Id);

            // Assert
            Assert.Equal("Updated", result.Name);
            Assert.Equal("Finished", result.Status);
        }

        [Fact]
        public async Task UpdateTaskAsync_ThrowsNotFound_WhenNotFound()
        {
            // Arrange
            var dto = new TaskUpdateDto { Name = "Error", Description = "Error", Status = "Error", UserId = Guid.NewGuid() };
            _taskRepository.Setup(r => r.UpdateTaskByIdAsync(It.IsAny<TaskModel>(), It.IsAny<Guid>()))
                           .ReturnsAsync((TaskModel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _taskService.UpdateTaskAsync(dto, Guid.NewGuid()));
        }

        [Fact]
        public async Task RemoveTaskAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _taskRepository.Setup(r => r.DeleteTaskByIdAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _taskService.RemoveTaskAsync(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveTaskAsync_ThrowsNotFound_WhenTaskNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _taskRepository.Setup(r => r.DeleteTaskByIdAsync(id)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _taskService.RemoveTaskAsync(id));
        }
    }
}
