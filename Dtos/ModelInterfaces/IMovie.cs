using System.Text.Json.Serialization;

namespace MovieApp.Dtos.ModelInterfaces;

public interface IMovie
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("overview")]
    public string? Overview { get; set; }

    [JsonPropertyName("poster")]
    public string? Poster { get; set; }

    [JsonPropertyName("play_until")]
    public DateTime PlayUntil { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }


    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
