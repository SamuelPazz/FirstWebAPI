using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Controllers;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Models.DTOs.Responses;
using ToDoApp.Models.Mappers;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<List<UserResponseDto>> ListAllUsersAsync()
        {
            List<UserModel> users = await _userRepository.FindAllUsersAsync();

            if (users.Count < 1)
            {
                _logger.LogError($"No users found");
                return new List<UserResponseDto>();
            }

            return users.Select(UserMapper.Of).ToList();
        }

        public async Task<UserResponseDto> UserByIdAsync(Guid id)
        {
            UserModel? user = await _userRepository.FindByIdAsync(id);

            if (user == null)
            {
                _logger.LogError($"User with id {id} not found.");
                throw new NotFoundException($"User with id {id} not found.");
            }

            return UserMapper.Of(user);
        }

        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto)
        {

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

            UserModel user = UserMapper.Of(userCreateDto);
            user.Password = hashedPassword;

            UserModel? createdUser = await _userRepository.SaveUserAsync(user);

            if (createdUser == null)
            {
                _logger.LogError($"User creation failed.");
                throw new BadRequestException("User creation failed.");
            }

            return UserMapper.Of(createdUser);
        }

        public async Task<UserResponseDto> UpdateUserAsync(UserUpdateDto userUpdateDto, Guid id)
        {
            UserModel? updatedUser = await _userRepository.UpdateUserByIdAsync(UserMapper.Of(userUpdateDto), id);

            if (updatedUser == null)
            {
                _logger.LogError($"User with id {id} not found for update.");
                throw new NotFoundException($"User with id {id} not found for update.");
            }

            return UserMapper.Of(updatedUser);
        }

        public async Task<bool> RemoveUserAsync(Guid id)
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
            UserModel? user = await _userRepository.FindyByEmailAsync(login.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                string token = _jwtTokenService.GenerateTokenJWT(user);
                return token;              
            }

            _logger.LogError("Invalid email or password");
            throw new NotFoundException("Invalid email or password");
        }
    }
}
