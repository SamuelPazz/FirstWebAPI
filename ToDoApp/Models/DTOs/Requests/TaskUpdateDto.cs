using System.ComponentModel.DataAnnotations;
using ToDoApp.Enums;
using ToDoApp.Models.DTOs.Responses;

namespace ToDoApp.Models.DTOs.Requests
{
    public record TaskUpdateDto
    {
        [Required(ErrorMessage = "name is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "name must have a maximum of 100 characters")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "description is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(1000, ErrorMessage = "name must have a maximum of 1000 characters")]
        public required string Description { get; init; }

        [Required(ErrorMessage = "status is required")]
        public required string Status { get; init; } 

        public Guid? UserId { get; init; }

        public TaskUpdateDto()
        {
        }

        public TaskUpdateDto(string name, string description, string status, Guid? userId)
        {
            Name = name;
            Description = description;
            Status = status;
            UserId = userId;
        }
    }
}
