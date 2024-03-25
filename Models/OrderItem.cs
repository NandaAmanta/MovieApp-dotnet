

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("order_items")]
    public class OrderItem : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("order_id")]
        public long OrderId { get; set; }
        public Order? Order { get; set; }


        [Column("movie_schedule_id")]
        public long MovieScheduleId { get; set; }
        public MovieSchedule? MovieSchedule { get; set; }


        [Column("qty")]
        public int Qty { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("sub_total_price")]
        public double SubTotalPrice { get; set; }
    }
}