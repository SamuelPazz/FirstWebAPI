using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Enums;

namespace ToDoApp.Models
{
    public class TaskModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("status")]
        public StatusTaskEnum Status { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        public virtual UserModel? User { get; set; }
    }
}
