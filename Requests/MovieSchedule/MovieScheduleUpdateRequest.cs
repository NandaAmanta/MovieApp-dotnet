using System.ComponentModel.DataAnnotations;

namespace MovieApp.Requests.MovieSchedule;

public class MovieScheduleUpdateRequest
{
    [Required]
    public required long MovieId { get; set; }

    [Required]
    public required long StudioId { get; set; }

    [Required]
    public required double Price { get; set; }

    [Required]
    public required string StartAt { get; set; }

    [Required]
    public required string EndAt { get; set; }
}