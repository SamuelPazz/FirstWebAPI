using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;
        private readonly JwtTokenService _jwtTokenService;


        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger, JwtTokenService jwtTokenService)
        {
            this._logger = logger;
            this._userRepository = userRepository;
            this._jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>?> GetAllUsersAsync()
        {
            try
            {
                List<UserModel>? users =  await _userRepository.FindAllUsersAsync();

                if (users == null)
                    return NotFound("Users not found");
                
                return Ok(users);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in GetAllUsersAsync: {ex}");
                return StatusCode(500, "An unexpected error occurred.");
            }          
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<UserModel>> GetUserByIdAsync(int id)
        {
            try
            {
                UserModel? findedUser = await _userRepository.FindByIdAsync(id);

                if(findedUser == null)
                    return NotFound("User not found");

                return Ok(findedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetUserByIdAsync: {ex}");

                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUserAsync([FromBody] UserModel user)
        {
            try
            {
                UserModel? createdUser = await _userRepository.SaveUserAsync(user);

                if (createdUser == null)
                    return NotFound("User not created or not found");

                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateUserAsync: {ex}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("ById/{id}")]
        public async Task<ActionResult<UserModel>> UpdateUserAsync([FromBody] UserModel user, int id)
        {
            try
            {
                user.Id = id;
                UserModel? findedUser = await _userRepository.UpdateUserByIdAsync(user, id);

                if (findedUser == null)
                    return NotFound("User not found");

                return Ok(findedUser);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in UpdateUserAsync: {ex}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("ById/{id}")]
        public async Task<ActionResult<bool>> DeleteUserAsync(int id)
        {
            try
            {
                bool result = await _userRepository.DeleteUserByIdAsync(id);

                if (result == false)
                    return NotFound("User not found to be deleted");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteUserByIdAsync: {ex}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
