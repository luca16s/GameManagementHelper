namespace iso.gmh.Core.Interfaces;

using System.Threading.Tasks;

using iso.gmh.Core.Models;

public interface IConnection<TClient> where TClient : class
{
    public TClient Client { get; }

    Task ConnectAsync(Secrets secrets);

    Task<UserModel> GetUserInformation();
}