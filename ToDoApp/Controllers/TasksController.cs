using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Repositories;
using ToDoApp.Repositories.Interfaces;
namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskRepository taskRepository, ILogger<TasksController> logger)
        {
            this._taskRepository = taskRepository;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTaskAsync([FromBody] TaskModel task)
        {
            try
            {
                TaskModel? createdTask = await _taskRepository.SaveTaskAsync(task);

                if (createdTask == null)
                    return StatusCode(404, "Task not created or not found");

                return StatusCode(201, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateTaskAsync {ex}");
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAllTasksAsync()
        {
            try
            {
                List<TaskModel>? tasks = await _taskRepository.FindAllTasksAsync();
                
                if (tasks == null) 
                    return StatusCode(204, "Tasks not found");

                return StatusCode(200, tasks);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllTasksAsync {ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskByIdAsync(int id)
        {
            try
            {
                TaskModel? findedTask = await _taskRepository.FindByIdAsync(id);

                if (findedTask == null)
                    return StatusCode(404, "Task not found");

                return StatusCode(200, findedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTaskByIdAsync {ex}");
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("ById/{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTaskAsync([FromBody] TaskModel task, int id)
        {
            try
            {
                task.Id = id;
                TaskModel? findedTask = await _taskRepository.UpdateTaskByIdAsync(task, id);

                if (findedTask == null)
                    return StatusCode(404, "Task not found");

                return StatusCode(200, findedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateTaskAsync {ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("ById/{id}")]
        public async Task<ActionResult<bool>> DeleteTaskAsync(int id)
        {
            try
            {
                bool result = await _taskRepository.DeleteTaskByIdAsync(id);

                if (!result)
                    return StatusCode(404, "Task not found to be deleted");
                
                return StatusCode(200, result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in DeleteTaskAsync {ex}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}