namespace GameSaveManager.Core.Models
{
    public class UserInfoModel
    {
        public UserInfoModel(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
