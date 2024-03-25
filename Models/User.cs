

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("users")]
    public class User : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long  Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }


        [Column("avatar")]
        public string? Avatar { get; set; }


        [Column("is_admin")]
        public bool IsAdmin { get; set; }
    }
}