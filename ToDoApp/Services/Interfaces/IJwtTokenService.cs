using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateTokenJWT(UserModel user);
    }
}
