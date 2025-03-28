using ToDoApp.Enums;

namespace ToDoApp.DTOs.Responses
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public StatusTaskEnum Status { get; set; }
        public Guid? UserId { get; set; }
        public UserResponseDto? User { get; set; }
    }
}
