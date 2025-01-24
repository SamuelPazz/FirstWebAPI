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

        public async Task<UserModel?> FindByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserModel>> FindAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> SaveUserAsync(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> UpdateUserByIdAsync(UserModel user, int id)
        {
            UserModel userById = await FindByIdAsync(id);

            if (userById == null)
                throw new Exception($"User not found by ID: {id}");

            userById.Name = user.Name;
            userById.Email = user.Email;
            userById.Password = user.Password;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            UserModel userById = await FindByIdAsync(id);

            if (userById == null)
                throw new Exception($"User not found by ID: {id}");

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
