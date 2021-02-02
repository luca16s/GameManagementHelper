using System.Text.Json.Serialization;

namespace GameSaveManager.Core.Models
{
    public class UserModel
    {
        [JsonPropertyName("givenName")]
        public string UserName { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public string Email { get; set; }

        public UserModel(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
