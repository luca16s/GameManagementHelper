using GameSaveManager.Core.Models;
using System.Threading.Tasks;

namespace GameSaveManager.Core.Interfaces
{
    public interface ICloudOperations
    {
        Task<bool> CreateFolder(string path);

        Task<bool> CheckFolderExistence(string folderName);

        Task<string> UploadSaveData(GameInformation gameInformation);

        Task<bool> DownloadSaveData(GameInformation gameInformation);
    }
}