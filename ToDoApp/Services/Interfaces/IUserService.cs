using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Models.DTOs.Responses;

namespace ToDoApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> ListAllUsersAsync();
        Task<UserResponseDto> UserByIdAsync(Guid id);
        Task<UserResponseDto> CreateUserAsync(UserCreateDto user);
        Task<UserResponseDto> UpdateUserAsync(UserUpdateDto userUpdateDto, Guid id);
        Task<bool> RemoveUserAsync(Guid id);
        Task<string> LoginAndAuthenticationAsync(UserLoginDTO login);
    }
}
