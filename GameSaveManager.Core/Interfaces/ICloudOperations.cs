using System.Threading.Tasks;

namespace GameSaveManager.Core.Interfaces
{
    public interface ICloudOperations
    {
        Task<bool> CreateFolder(string path);
        Task<bool> CheckFolderExistence(string folderName);
        Task<string> UploadSaveData(string filePath, string folder, string fileName);
        bool DownloadSaveData();
    }
}
