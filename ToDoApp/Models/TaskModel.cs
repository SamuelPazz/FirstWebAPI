using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Enums;

namespace ToDoApp.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = StatusTaskEnum.Pending.ToString();

        [Column("user_id")]
        public Guid? UserId { get; set; }
        public UserModel? User { get; set; }

        public TaskModel()
        {
        }

        public TaskModel(Guid id, string name, string description, StatusTaskEnum status, Guid? userId, UserModel? user)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status.ToString();
            UserId = userId;
            User = user;
        }
    }
}
