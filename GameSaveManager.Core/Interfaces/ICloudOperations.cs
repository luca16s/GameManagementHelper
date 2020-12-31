namespace GameSaveManager.Core.Interfaces
{
    using GameSaveManager.Core.Models;

    using System.Threading.Tasks;

    public interface ICloudOperations
    {
        Task<bool> CreateFolder(string path);

        Task<bool> CheckFolderExistence(string folderName);

        Task<bool> UploadSaveData(GameInformationModel gameInformation);

        Task<bool> DownloadSaveData(GameInformationModel gameInformation);
    }
}