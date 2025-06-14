using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.DTOs.Requests
{
    public record UserCreateDto
    {
        [Required(ErrorMessage = "name is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "name must have a maximum of 100 characters")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "email is required")]
        [MaxLength(100, ErrorMessage = "email must have a maximum of 100 characters")]
        [EmailAddress(ErrorMessage = "email must have @domain.extension")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "password is required")]
        [MinLength(3, ErrorMessage = "password must have a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "password must have a maximum of 100 characters")]
        public required string Password { get; init; }
    }
}
