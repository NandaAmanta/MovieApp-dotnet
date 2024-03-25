

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Authentication.Requests;

public class LoginRequest
{

    [JsonPropertyName("email")]
    [Required, EmailAddress]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    [Required, MinLength(8)]
    public required string Password { get; set; }
}