using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Enums;

namespace ToDoApp.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "name is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "name must have a maximum of 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "description is required")]
        [MinLength(3, ErrorMessage = "name must have a minimum of 3 characters")]
        [MaxLength(1000, ErrorMessage = "name must have a maximum of 1000 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "status is required")]
        public required StatusTaskEnum Status { get; set; }

        [Column("user_id")]
        public Guid? UserId { get; set; }
        public virtual UserModel? User { get; set; }
    }
}
