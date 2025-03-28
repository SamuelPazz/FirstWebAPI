using ToDoApp.DTOs.Requests;
using ToDoApp.DTOs.Responses;
using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> ListAllUsersAsync();
        Task<UserResponseDto> UserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(UserModel user);
        Task<UserResponseDto> UpdateUserAsync(UserModel user, int id);
        Task<bool> RemoveUserAsync(int id);
        Task<string> LoginAndAuthenticationAsync(UserLoginDTO login);
    }
}
