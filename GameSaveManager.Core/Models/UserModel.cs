namespace GameSaveManager.Core.Models
{
    using System.Text.Json.Serialization;

    public class UserModel
    {
        public UserModel(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }

        [JsonPropertyName("userPrincipalName")]
        public string Email { get; set; }

        [JsonPropertyName("givenName")]
        public string UserName { get; set; }
    }
}