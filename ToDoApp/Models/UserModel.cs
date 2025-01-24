using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class UserModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("password")]
        public string? Password { get; set; }
    }
}
