using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Models.DTOs.Responses;
using ToDoApp.Models.Mappers;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            List<UserResponseDto> users = await _userService.ListAllUsersAsync();

            if (users.Count == 0)            
                return StatusCode(204); 

            return StatusCode(200, users); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            UserResponseDto user = await _userService.UserByIdAsync(id);
            
            return StatusCode(200, user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDto userDto)
        {
            UserResponseDto createdUser = await _userService.CreateUserAsync(userDto);

            return StatusCode(201, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserUpdateDto userUpdateDto)
        {
            UserResponseDto updatedUser = await _userService.UpdateUserAsync(userUpdateDto, id);
            return StatusCode(200, updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            bool result = await _userService.RemoveUserAsync(id);
            return StatusCode(200, result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO login)
        {
            string token = await _userService.LoginAndAuthenticationAsync(login);
            return StatusCode(200, token); 
        }        
    }
}
