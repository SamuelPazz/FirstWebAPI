using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.DTOs;
using ToDoApp.DTOs.Requests;
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

        public async Task<List<UserModel>?> FindAllUsersAsync()
        {
            List<UserModel> users =  await _dbContext.Users.ToListAsync();

            if (!users.Any()) 
                return null;

            return users;
        }

        public async Task<UserModel?> FindByIdAsync(int id)
        {       
            UserModel? user =  await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

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

        public async Task<UserModel?> UpdateUserByIdAsync(UserModel user, int id)
        {
            UserModel? userById = await FindByIdAsync(id);

            if (userById == null)
                return null;

            userById.Name = user.Name;
            userById.Email = user.Email;
            userById.Password = user.Password;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            UserModel? userById = await FindByIdAsync(id);

            if (userById == null)
                return false;

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel?> FindyByLogin(UserLoginDTO login)
        {
            UserModel? user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == login.Password);

            if (user == null)
                return null;

            return user;
        }
    }
}
