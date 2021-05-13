namespace GameSaveManager.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GameSaveManager.Core.Models;

    public interface ICloudOperations
    {
        Task<bool> CheckFolderExistence(string folderName);

        Task<bool> CreateFolder(string path);

        Task<bool> DeleteSave(string path);

        Task<bool> DownloadSaveData(GameInformationModel gameInformation);

        Task<IEnumerable<(string name, string path)>> GetSavesList(GameInformationModel gameInformation);

        Task<bool> UploadSaveData(GameInformationModel gameInformation, bool overwriteSave);
    }
}