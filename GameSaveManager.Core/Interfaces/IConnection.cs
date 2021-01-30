namespace GameSaveManager.Core.Interfaces
{
    using System.Threading.Tasks;

    using GameSaveManager.Core.Models;

    public interface IConnection
    {
        Task<dynamic> ConnectAsync(Secrets secrets);
    }
}