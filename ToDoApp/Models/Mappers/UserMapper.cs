using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Models.DTOs.Responses;

namespace ToDoApp.Models.Mappers
{
    public class UserMapper
    {
        public static UserModel Of(UserCreateDto dto)
        {
            UserModel user = new UserModel() 
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            return user;
        }

        public static UserResponseDto Of(UserModel user)
        {
            UserResponseDto dto = new UserResponseDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            }; 
            
            return dto;
        }

        public static UserModel Of(UserUpdateDto dto)
        {
            UserModel user = new UserModel();
            user.Name = dto.Name;
            user.Email = dto.Email;

            return user;
        }
    }
}
