

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Authentication.Requests;
using MovieApp.Dtos;

namespace MovieApp.Models
{
    [Table("users")]
    public class User : BaseEntity, IUser
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }


        [Column("is_admin")]
        public bool IsAdmin { get; set; } = false;

        public User SetHashedPassword(string rawPassword)
        {
            this.Password = BCrypt.Net.BCrypt.HashPassword(rawPassword);
            return this;
        }

        public bool CheckPassword(string rawPassword){
            return BCrypt.Net.BCrypt.Verify(rawPassword,this.Password);
        }

    }
}