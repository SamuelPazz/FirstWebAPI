using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.DTOs.Requests
{
    public record UserUpdateDto
    {
        [Required(ErrorMessage = "name is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "name must have a maximum of 100 characters")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "email is required")]
        [MaxLength(100, ErrorMessage = "email must have a maximum of 100 characters")]
        [EmailAddress(ErrorMessage = "email must have @domain.extension")]
        public required string Email { get; init; }

        public UserUpdateDto()
        {
        }

        public UserUpdateDto(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
