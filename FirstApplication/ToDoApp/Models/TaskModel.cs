using System.ComponentModel.DataAnnotations.Schema;

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
        public int Status { get; set; }
    }
}
