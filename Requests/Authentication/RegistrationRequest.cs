

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApp.Requests.Authentication;

public class RegistrationRequest
{

    [JsonPropertyName("email")]
    [Required, EmailAddress, MaxLength(100)]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    [Required, MinLength(8),MaxLength(100)]
    public required string Password { get; set; }

    [JsonPropertyName("name")]
    [Required, MinLength(1),MaxLength(100)]
    public required string Name { get; set; }
}