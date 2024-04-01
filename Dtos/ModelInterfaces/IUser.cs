

using System.Text.Json.Serialization;

namespace MovieApp.Dtos.ModelInterfaces;

public interface IUser
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }


    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
