namespace iso.gmh.Core.Interfaces
{
    using System.Threading.Tasks;

    using iso.gmh.Core.Models;

    public interface IConnection
    {
        public dynamic PublicClientApp { get; }

        Task ConnectAsync(Secrets secrets);

        Task<UserModel> GetUserInformation();
    }
}