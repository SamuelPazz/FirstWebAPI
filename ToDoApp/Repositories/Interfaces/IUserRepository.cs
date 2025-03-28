using ToDoApp.DTOs.Requests;
using ToDoApp.Models;

namespace ToDoApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>?> FindAllUsersAsync();
        Task<UserModel?> FindByIdAsync(int id);
        Task<UserModel?> SaveUserAsync(UserModel user);
        Task<UserModel?> UpdateUserByIdAsync(UserModel user, int id);
        Task<bool> DeleteUserByIdAsync(int id);
        Task<UserModel?> FindyByLogin(UserLoginDTO login);
    }
}
