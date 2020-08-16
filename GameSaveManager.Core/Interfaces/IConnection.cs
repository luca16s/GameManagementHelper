using System.Threading.Tasks;

namespace GameSaveManager.Core.Interfaces
{
    public interface IConnection
    {
        Task<object> ConnectAsync(string appKey, string appSecret);
    }
}