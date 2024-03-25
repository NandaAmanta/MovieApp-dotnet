

using System.Text.Json.Serialization;

namespace MovieApp.Dtos;

public interface IUser
{
    [JsonPropertyName("id")]
    public long Id { get; set; }


    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }


    [JsonPropertyName("is_admin")]
    public long IsAdmin { get; set; }

    [JsonPropertyName("email")]
    public long Email { get; set; }
}
