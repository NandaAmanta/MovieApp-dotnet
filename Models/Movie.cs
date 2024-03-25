

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("movies")]
    public class Movie : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("overview")]
        public string? Overview { get; set; }

        [Column("poster")]
        public string? Poster { get; set; }

        [Column("play_until")]
        public DateTime PlayUntil { get; set; }
    }
}