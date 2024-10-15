namespace iso.gmh.Core.Interfaces;

using System.Threading.Tasks;

using iso.gmh.Core.Models;

public interface IConnection<TClient> where TClient : class
{
    /// <summary>
    /// Cliente do serviço da nuvem.
    /// </summary>
    public TClient Client { get; }

    /// <summary>
    /// Realiza a conexão do o serviço de nuvem.
    /// </summary>
    /// <param name="secrets">
    /// User secrets para conexão.
    /// </param>
    /// <returns>
    /// O client conectado.
    /// </returns>
    Task ConnectAsync(
        Secrets secrets
    );

    /// <summary>
    /// Busca as informações do usuário.
    /// </summary>
    /// <returns>
    /// Informações do Usuário.
    /// </returns>
    Task<User> GetUserInformationAsync();
}