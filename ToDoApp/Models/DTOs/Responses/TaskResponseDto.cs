using ToDoApp.Enums;

namespace ToDoApp.Models.DTOs.Responses
{
    public record TaskResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = StatusTaskEnum.Pending.ToString();
        public UserResponseDto? User { get; init; }

        public TaskResponseDto() { }

        public TaskResponseDto(Guid id, string name, string description, StatusTaskEnum status, UserResponseDto? user)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status.ToString();
            User = user;
        }
    }
}
