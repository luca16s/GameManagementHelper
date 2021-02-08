namespace GameSaveManager.Core.Models
{
    using System.Text.Json.Serialization;

    using Flunt.Notifications;
    using Flunt.Validations;

    using GameSaveManager.Core.Utils;

    public class UserModel : Notifiable
    {
        [JsonPropertyName("givenName")]
        public string UserName { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public string Email { get; set; }

        public UserModel(string userName, string email)
        {
            Clear();

            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(userName, nameof(UserName), SystemMessages.UserNameCannotBeNullMessage)
                .IsNotNullOrWhiteSpace(email, nameof(Email), SystemMessages.EmailCannotBeNullMessage));

            if (Valid)
            {
                UserName = userName;
                Email = email;
            }
        }
    }
}