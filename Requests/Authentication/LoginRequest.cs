

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Requests.Authentication;

public class LoginRequest
{

    [JsonPropertyName("email")]
    [Required]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    [Required]
    public required string Password { get; set; }
}