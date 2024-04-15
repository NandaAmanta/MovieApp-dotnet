

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Requests.Authentication;

public class LoginRequest
{

    [JsonPropertyName("email")]
    [MaxLength(100)]
    [Required]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    [MaxLength(100)]
    [MinLength(8)]
    [Required]
    public required string Password { get; set; }
}