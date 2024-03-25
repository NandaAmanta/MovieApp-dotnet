

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("movie_tags")]
    public class MovieTag : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("movie_id")]
        public long MovieId { get; set; }
        public Movie? Movie { get; set; }

        
        [Column("tag_id")]
        public long TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}