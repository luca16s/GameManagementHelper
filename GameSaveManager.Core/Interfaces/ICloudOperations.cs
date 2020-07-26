using System.Threading.Tasks;

namespace GameSaveManager.Core.Interfaces
{
    public interface ICloudOperations
    {
        Task<string> UploadSaveData(string filePath, string folder, string fileName);
        bool DownloadSaveData();
        Task<bool> CheckFolderExistence(string folderName);
        Task<bool> CreateFolder(string path);
    }
}
