

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("studios")]
    public class Studio : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("studio_number")]
        public string? StudioNumber { get; set; }

        [Column("seat_capacity")]
        public int? SeatCapacity { get; set; }
    }
}