namespace GameSaveManager.Core.Models
{
    public class UserModel
    {
        public string UserName { get; }
        public string Email { get; }

        public UserModel(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
