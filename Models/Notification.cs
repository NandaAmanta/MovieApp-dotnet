

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Enums;

namespace MovieApp.Models
{
    [Table("notifications")]
    public class Notification : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("message")]
        public string? Message { get; set; }

        [Column("type")]
        public NotificationType Type { get; set; }

        [Column("is_read")]
        public bool IsRead { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }
        public User? User { get; set; }
    }
}