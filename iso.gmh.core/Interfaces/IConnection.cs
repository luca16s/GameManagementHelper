namespace iso.gmh.Core.Interfaces;

using System.Threading.Tasks;

using iso.gmh.Core.Models;

public interface IConnection<TClient> where TClient : class
{
    public TClient Client { get; set; }

    /// <summary>
    /// Realiza a conexão do o serviço de nuvem.
    /// </summary>
    /// <param name="secrets">
    /// User secrets para conexão.
    /// </param>
    /// <returns>
    /// O client conectado.
    /// </returns>
    Task<TClient> ConnectAsync(
        Secrets secrets
    );

    /// <summary>
    /// Busca as informações do usuário.
    /// </summary>
    /// <returns>
    /// Informações do Usuário.
    /// </returns>
    Task<User> GetUserInformation();
}