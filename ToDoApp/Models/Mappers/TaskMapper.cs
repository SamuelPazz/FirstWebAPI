using System.Xml.Linq;
using ToDoApp.Enums;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Models.DTOs.Responses;

namespace ToDoApp.Models.Mappers
{
    public class TaskMapper
    {
        public static TaskResponseDto Of(TaskModel task)
        {
            UserResponseDto? user = task.User == null ? null : UserMapper.Of(task.User);

            TaskResponseDto dto = new TaskResponseDto()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                User = user
            };

            return dto;
        }

        public static TaskModel Of(TaskCreateDto taskCreateDto)
        {
            return new TaskModel()
            {
                Name = taskCreateDto.Name,
                Description = taskCreateDto.Description
            };
        }

        public static TaskModel Of(TaskUpdateDto taskUpdateDto)
        {
            return new TaskModel()
            {
                Name = taskUpdateDto.Name,
                Description = taskUpdateDto.Description,
                Status = taskUpdateDto.Status.ToString(),
                UserId = taskUpdateDto.UserId,
            };            
        }
    }
}
