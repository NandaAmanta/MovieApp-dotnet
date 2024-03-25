

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("movie_schedules")]
    public class MovieSchedule : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("movie_id")]
        public long MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Column("studio_id")]
        public long StudioId { get; set; }
        public Studio? Studio { get; set; }

        [Column("start_at")]
        public DateTime StartAt { get; set; }

        [Column("end_at")]
        public DateTime EndAt { get; set; }

        [Column("price")]
        public double price { get; set; }

    }
}