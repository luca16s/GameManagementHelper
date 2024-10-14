namespace iso.gmh.Core.Models;

using System.Text.Json.Serialization;

public class User(
    string userName,
    string email
)
{
    [JsonPropertyName("givenName")]
    public string UserName { get; set; } = userName;

    [JsonPropertyName("userPrincipalName")]
    public string Email { get; set; } = email;
}