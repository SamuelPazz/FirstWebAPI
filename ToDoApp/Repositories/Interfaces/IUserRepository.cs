using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;

namespace ToDoApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>?> FindAllUsersAsync();
        Task<UserModel?> FindByIdAsync(Guid id);
        Task<UserModel?> SaveUserAsync(UserModel user);
        Task<UserModel?> UpdateUserByIdAsync(UserModel user, Guid id);
        Task<bool> DeleteUserByIdAsync(Guid id);
        Task<UserModel?> FindyByLogin(UserLoginDTO login);
    }
}
