using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("getUsers")]
        public async Task<ActionResult<List<UserModel>>> GetAllUsersAsync()
        {
            List<UserModel> users =  await userRepository.FindAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("getUserById/{id}")]
        public async Task<ActionResult<UserModel>> GetUserByIdAsync(int id)
        {
            UserModel findedUser = await userRepository.FindByIdAsync(id);
            return Ok(findedUser);
        }
            
        [HttpPost("addUser")]
        public async Task<ActionResult<UserModel>> CreateUserAsync([FromBody] UserModel user)
        {
            UserModel createdUser = await userRepository.SaveUserAsync(user);
            return Ok(createdUser);
        }

        [HttpPut("updateUser/{id}")]
        public async Task<ActionResult<UserModel>> UpdateUserAsync([FromBody] UserModel user, int id)
        {
            user.Id = id;
            UserModel findedUser = await userRepository.UpdateUserByIdAsync(user, id);

            return Ok(findedUser);
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult<UserModel>> DeleteUserAsync(int id)
        {
            bool result = await userRepository.DeleteUserByIdAsync(id);

            return Ok(result);
        }

    }
}
