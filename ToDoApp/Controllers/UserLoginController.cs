using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("/api/[controller]/")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserLoginController> _logger;
        private readonly JwtTokenService _jwtTokenService;

        public UserLoginController(IUserRepository userRepository, ILogger<UserLoginController> logger, JwtTokenService jwtTokenService)
        {
            this._logger = logger;
            this._userRepository = userRepository;
            this._jwtTokenService = jwtTokenService;
        }


        [HttpPost]
        public async Task<ActionResult<UserModel>> Login([FromBody] UserLoginDTO login)
        {
            try
            {
                var user = await _userRepository.FindyByLogin(login);

                if (user == null)
                    return NotFound("User not found");

                var token = _jwtTokenService.GenerateTokenJWT(user);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Login: {ex}");
                return StatusCode(500, ex.Message);
            }

        }
    }
}
