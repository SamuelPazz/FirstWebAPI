using Microsoft.Extensions.Logging;
using Moq;
using ToDoApp;
using ToDoApp.Controllers;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services;
using ToDoApp.Services.Interfaces;

namespace ToDoAppTest
{
    public class UserTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IJwtTokenService> _jwtTokenService;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly IUserService _userService;

        public UserTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _jwtTokenService = new Mock<IJwtTokenService>();
            _logger = new Mock<ILogger<UserService>>();

            _userService = new UserService(
                _userRepository.Object,
                _jwtTokenService.Object,
                _logger.Object
                );
        }

        [Fact]
        public async void ListAllUsersAsync_ReturnsListOfUserResponseDto_WhenUsersExist()
        {
            //Arrange
            var users = new List<UserModel>
            {
                new() { Id = Guid.NewGuid(), Name = "Lightning McQueen", Email = "mcqueen@email.com", Password = "password123" },
                new() { Id = Guid.NewGuid(), Name = "Tommy Shelby", Email = "shelby@email.com", Password = "password123" }
            };        
            _userRepository.Setup(r => r.FindAllUsersAsync()).ReturnsAsync(users);

            //Act
            var result = await _userService.ListAllUsersAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListAllUsersAsync_ReturnsEmpty_WhenNoUsersExist()
        {
            //Arrange
            _userRepository.Setup(r => r.FindAllUsersAsync()).ReturnsAsync(new List<UserModel>());

            //Act
            var result = await _userService.ListAllUsersAsync();
            
            //Assert 
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UserByIdAsync_ReturnsUser_WhenExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new UserModel { Id = id, Name = "Lightning McQueen", Email = "mcqueen@email.com" };
            _userRepository.Setup(r => r.FindByIdAsync(user.Id)).ReturnsAsync(user);

            //Act
            var result = await _userService.UserByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Lightning McQueen", result.Name);
        }

        [Fact]
        public async Task UserByIdAsync_ThrowsNotFound_WhenNotExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            _userRepository.Setup(r => r.FindByIdAsync(id)).ReturnsAsync((UserModel?)null);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _userService.UserByIdAsync(id));
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsCreatedUser_WhenSuccessful()
        {
            //Arrange
            var dto = new UserCreateDto { Name = "Lightning McQueen", Email = "mcqueen@email.com", Password = "password123" };
            var user = new UserModel { Id = Guid.NewGuid(), Name = "Lightning McQueen", Email = "mcqueen@email.com", Password = "hash" };
            _userRepository.Setup(r => r.SaveUserAsync(It.IsAny<UserModel>())).ReturnsAsync(user);

            //Act
            var result = await _userService.CreateUserAsync(dto);

            //Assert
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task CreateUserAsync_ThrowsBadRequest_WhenFailsToSave()
        {
            //Arrange
            var dto = new UserCreateDto { Name = "Lightning McQueen", Email = "mcqueen@email.com", Password = "password123" };
            _userRepository.Setup(r => r.SaveUserAsync(It.IsAny<UserModel>())).ReturnsAsync((UserModel?)null);

            //Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _userService.CreateUserAsync(dto));
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsUpdatedUser_WhenSuccessful()
        {
            //Arrange
            var dto = new UserUpdateDto { Name = "Updated Name", Email = "updated@email.com" };
            var updated = new UserModel { Id = Guid.NewGuid(), Name = dto.Name, Email = dto.Email };
            _userRepository.Setup(r => r.UpdateUserByIdAsync(It.IsAny<UserModel>(), It.IsAny<Guid>())).ReturnsAsync(updated);

            //Act
            var result = await _userService.UpdateUserAsync(dto, updated.Id);

            //Assert
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public async Task UpdateUserAsync_ThrowsNotFound_WhenUserNotFound()
        {
            //Arrange
            var dto = new UserUpdateDto { Name = "Test Name", Email = "test@email.com" };
            _userRepository.Setup(r => r.UpdateUserByIdAsync(It.IsAny<UserModel>(), It.IsAny<Guid>())).ReturnsAsync((UserModel?)null);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _userService.UpdateUserAsync(dto, Guid.NewGuid()));
        }

        [Fact]
        public async Task RemoveUserAsync_ReturnsTrue_WhenSuccessful()
        {
            //Arrange
            var id = Guid.NewGuid();
            _userRepository.Setup(r => r.DeleteUserByIdAsync(id)).ReturnsAsync(true);

            //Act
            var result = await _userService.RemoveUserAsync(id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveUserAsync_ThrowsNotFound_WhenUserDoesNotExist()
        {
            //Arrange
            var id = Guid.NewGuid();
            _userRepository.Setup(r => r.DeleteUserByIdAsync(id)).ReturnsAsync(false);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _userService.RemoveUserAsync(id));
        }

        [Fact]
        public async Task LoginAndAuthenticationAsync_ReturnsToken_WhenSuccessful()
        {
            //Arrange
            var dto = new UserLoginDTO { Email = "login@email.com", Password = "password123" };
            var user = new UserModel { Id = Guid.NewGuid(), Email = dto.Email, Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) };
            _userRepository.Setup(r => r.FindyByEmailAsync(dto.Email)).ReturnsAsync(user);
            _jwtTokenService.Setup(s => s.GenerateTokenJWT(user)).Returns("fake_token");

            //Act
            var token = await _userService.LoginAndAuthenticationAsync(dto);

            //Assert
            Assert.Equal("fake_token", token);
        }

        [Fact]
        public async Task LoginAndAuthenticationAsync_Throws_WhenPasswordWrong()
        {
            //Arrange
            var dto = new UserLoginDTO { Email = "user@email.com", Password = "wrongpass" };
            var user = new UserModel { Email = dto.Email, Password = BCrypt.Net.BCrypt.HashPassword("realpass") };
            _userRepository.Setup(r => r.FindyByEmailAsync(dto.Email)).ReturnsAsync(user);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _userService.LoginAndAuthenticationAsync(dto));
        }

        [Fact]
        public async Task LoginAndAuthenticationAsync_Throws_WhenUserNotFound()
        {
            //Arrange
            var dto = new UserLoginDTO { Email = "notfound@email.com", Password = "password123" };
            _userRepository.Setup(r => r.FindyByEmailAsync(dto.Email)).ReturnsAsync((UserModel?)null);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _userService.LoginAndAuthenticationAsync(dto));
        }
    }
}