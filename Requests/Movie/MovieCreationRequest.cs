

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Requests.Movie;

public class MovieCreationRequest
{

    [JsonPropertyName("title")]
    [Required]
    public required string Title { get; set; }

    [JsonPropertyName("overview")]
    [Required]
    public required string Overview { get; set; }

    [JsonPropertyName("poster")]
    [Required]
    public required IFormFile PosterFile { get; set; }

    [Required]
    public required string PlayUntil { get; set; }
}