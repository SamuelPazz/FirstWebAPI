using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
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

        public async Task<List<UserModel>> FindAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();

        }

        public async Task<UserModel?> FindByIdAsync(Guid id)
        {       
            UserModel? user =  await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<UserModel?> SaveUserAsync(UserModel user)
        {
            if (user == null)
                return null;

            await _dbContext.Users.AddAsync(user);          
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel?> UpdateUserByIdAsync(UserModel user, Guid id)
        {
            UserModel? userById = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userById == null)
                return null;

            userById.Name = user.Name;
            userById.Email = user.Email;

            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            UserModel? userById = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userById == null)
                return false;

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel?> FindyByEmailAsync(string email)
        {
            UserModel? user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
