

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Requests.Movie;

public class MovieUpdateRequest
{

    [JsonPropertyName("title")]
    [Required,MaxLength(100)]
    public required string Title { get; set; }

    [JsonPropertyName("overview")]
    [Required,MaxLength(250)]
    public required string Overview { get; set; }

    [JsonPropertyName("poster")]
    public IFormFile? PosterFile { get; set; }


    [Required]
    public required string PlayUntil { get; set; }
}