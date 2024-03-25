
using System.Text.Json.Serialization;

namespace MovieApp.Dtos;

public class Token(string accessToken, string refreshToken)
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = accessToken;


    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = refreshToken;
}