

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Requests.Movie;

public class MovieUpdateRequest
{

    [JsonPropertyName("title")]
    [Required]
    public required string Title { get; set; }

    [JsonPropertyName("overview")]
    [Required]
    public required string Overview { get; set; }

    [JsonPropertyName("poster")]
    public IFormFile? PosterFile { get; set; }
}