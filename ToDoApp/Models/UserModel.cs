using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();

        public UserModel(){}

        public UserModel(Guid id, string name, string email, string password, ICollection<TaskModel> tasks)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Tasks = tasks;
        }
    }
}
