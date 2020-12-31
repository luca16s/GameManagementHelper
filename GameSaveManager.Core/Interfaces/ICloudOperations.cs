namespace GameSaveManager.Core.Interfaces
{
    using System.Threading.Tasks;

    using GameSaveManager.Core.Models;

    public interface ICloudOperations
    {
        Task<bool> CreateFolder(string path);

        Task<bool> CheckFolderExistence(string folderName);

        Task<bool> UploadSaveData(GameInformationModel gameInformation);

        Task<bool> DownloadSaveData(GameInformationModel gameInformation);
    }
}