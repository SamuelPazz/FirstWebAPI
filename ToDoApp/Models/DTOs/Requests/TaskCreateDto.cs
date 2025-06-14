using System.ComponentModel.DataAnnotations;
using ToDoApp.Enums;

namespace ToDoApp.Models.DTOs.Requests
{
    public record TaskCreateDto
    {
        [Required(ErrorMessage = "name is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "name must have a maximum of 100 characters")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "description is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(1000, ErrorMessage = "name must have a maximum of 1000 characters")]
        public required string Description { get; init; }

        public TaskCreateDto()
        {
        }

        public TaskCreateDto(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
