namespace ToDoApp.Models.DTOs.Responses
{
    public record UserResponseDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }

        public UserResponseDto()
        {
        }

        public UserResponseDto(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
