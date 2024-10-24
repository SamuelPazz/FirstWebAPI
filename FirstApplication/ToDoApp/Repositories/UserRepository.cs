using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoAppDBContext _dbContext;

        public UserRepository(ToDoAppDBContext toDoAppDBContext)
        {
            _dbContext = toDoAppDBContext;
        }

        public async Task<UserModel?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> UpdateUserAsync(UserModel user, int id)
        {
            UserModel userById = await GetUserByIdAsync(id);

            if (userById == null)
                throw new Exception($"User not found by ID: {id}");

            userById.Name = user.Name;
            userById.Email = user.Email;
            userById.Password = user.Password;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            UserModel userById = await GetUserByIdAsync(id);

            if (userById == null)
                throw new Exception($"User not found by ID: {id}");

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
