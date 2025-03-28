namespace ToDoApp.DTOs.Responses
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
