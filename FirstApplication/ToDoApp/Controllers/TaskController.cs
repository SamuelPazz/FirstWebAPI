using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Repositories;
using ToDoApp.Repositories.Interfaces;
namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        public TaskController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        [HttpGet("getTasks")]
        public async Task<ActionResult<List<TaskModel>>> GetAllTasksAsync()
        {
            List<TaskModel> tasks = await taskRepository.FindAllTasksAsync();
            return Ok(tasks);
        }
        [HttpGet("getTaskById/{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskByIdAsync(int id)
        {
            TaskModel findedTask = await taskRepository.FindByIdAsync(id);
            return Ok(findedTask);
        }

        [HttpPost("addTask")]
        public async Task<ActionResult<TaskModel>> CreateTaskAsync([FromBody] TaskModel task)
        {
            TaskModel createdTask = await taskRepository.SaveTaskAsync(task);
            return Ok(createdTask);
        }
        [HttpPut("updateTask/{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTaskAsync([FromBody] TaskModel task, int id)
        {
            task.Id = id;
            TaskModel findedTask = await taskRepository.UpdateTaskByIdAsync(task, id);
            return Ok(findedTask);
        }
        [HttpDelete("deleteTask/{id}")]
        public async Task<ActionResult<TaskModel>> DeleteTaskAsync(int id)
        {
            bool result = await taskRepository.DeleteTaskByIdAsync(id);
            return Ok(result);
        }
    }
}