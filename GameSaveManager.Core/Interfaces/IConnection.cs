using GameSaveManager.Core.Models;

using System.Threading.Tasks;

namespace GameSaveManager.Core.Interfaces
{
    public interface IConnection
    {
        Task<object> ConnectAsync(Secrets secrets);
    }
}