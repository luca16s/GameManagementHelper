namespace GameSaveManager.Core.Interfaces
{
    using GameSaveManager.Core.Models;

    using System.Threading.Tasks;

    public interface IConnection
    {
        Task<object> ConnectAsync(Secrets secrets);
    }
}