using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Controllers;
using ToDoApp.DTOs.Requests;
using ToDoApp.DTOs.Responses;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<UsersController> _logger;

        public UserService(IUserRepository userRepository, JwtTokenService jwtTokenService, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<List<UserResponseDto>> ListAllUsersAsync()
        {
            List<UserModel>? users = await _userRepository.FindAllUsersAsync();

            if (users == null || !users.Any())
            {
                _logger.LogError($"No users found");
                return new List<UserResponseDto>();
            }

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }

        public async Task<UserResponseDto> UserByIdAsync(int id)
        {
            UserModel? user = await _userRepository.FindByIdAsync(id);

            if (user == null)
            {
                _logger.LogError($"User with id {id} not found.");
                throw new NotFoundException($"User with id {id} not found.");
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserResponseDto> CreateUserAsync(UserModel user)
        {
            UserModel? createdUser = await _userRepository.SaveUserAsync(user);

            if (createdUser == null)
            {
                _logger.LogError($"User creation failed.");
                throw new BadRequestException("User creation failed.");
            }

            return new UserResponseDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(UserModel user, int id)
        {
            UserModel? updatedUser = await _userRepository.UpdateUserByIdAsync(user, id);

            if (updatedUser == null)
            {
                _logger.LogError($"User with id {id} not found for update.");
                throw new NotFoundException($"User with id {id} not found for update.");
            }

            return new UserResponseDto
            {
                Id = updatedUser.Id,
                Name = updatedUser.Name,
                Email = updatedUser.Email
            };
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            bool result = await _userRepository.DeleteUserByIdAsync(id);

            if (!result)
            {
                _logger.LogError($"User with id {id} not found for deletion.");
                throw new NotFoundException($"User with id {id} not found for deletion.");
            }
            
            return result;
        }

        public async Task<string> LoginAndAuthenticationAsync(UserLoginDTO login)
        {
            UserModel? user = await _userRepository.FindyByLogin(login);

            if (user == null)
            {
                _logger.LogError($"User not found");
                throw new NotFoundException("User not found");
            }

            string token = _jwtTokenService.GenerateTokenJWT(user);

            return token;            
        }
    }
}
