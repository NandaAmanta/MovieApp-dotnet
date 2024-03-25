

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Enums;

namespace MovieApp.Models
{
    [Table("orders")]
    public class Order : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }
        public User? User { get; set; }

        [Column("total_item_price")]
        public double TotalItemPrice { get; set; }

        [Column("payment_method")]
        public PaymentMethod? PaymentMethod { get; set; }
    }
}