namespace GameSaveManager.Core.Interfaces
{
    using System.Threading.Tasks;

    using GameSaveManager.Core.Models;

    public interface IConnection
    {
        Task<object> ConnectAsync(Secrets secrets);
    }
}