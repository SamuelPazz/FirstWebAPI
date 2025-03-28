using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs.Responses;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            List<TaskResponseDto> tasks = await _taskService.ListAllTasksAsync();

            if (!tasks.Any())
                return StatusCode(204); 

            return StatusCode(200, tasks); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync(Guid id)
        {
            TaskResponseDto task = await _taskService.TaskByIdAsync(id);

            return StatusCode(200, task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskAsync([FromBody] TaskModel task)
        {
            TaskResponseDto createdTask = await _taskService.CreateTaskAsync(task);

            return StatusCode(201, createdTask); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAsync(Guid id, [FromBody] TaskModel task)
        {
            TaskResponseDto updatedTask = await _taskService.UpdateTaskAsync(task, id);

            return StatusCode(200, updatedTask); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            bool result = await _taskService.RemoveTaskAsync(id);

            return StatusCode(200, result);
        }
    }
}
