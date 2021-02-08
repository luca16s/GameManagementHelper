namespace GameSaveManager.Core.Interfaces
{
    using System.Threading.Tasks;

    using GameSaveManager.Core.Models;

    public interface IConnection
    {
        public dynamic PublicClientApp { get; }

        Task ConnectAsync(Secrets secrets);

        Task<UserModel> GetUserInformation();
    }
}