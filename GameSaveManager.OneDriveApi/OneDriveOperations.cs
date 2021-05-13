namespace GameSaveManager.OneDriveApi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;

    using Microsoft.Identity.Client;

    public class OneDriveOperations : ICloudOperations
    {
        private readonly IBackupStrategy BackupStrategy;
        private readonly IPublicClientApplication Client;

        public OneDriveOperations(IBackupStrategy backupStrategy, IPublicClientApplication client)
        {
            BackupStrategy = backupStrategy;
            Client = client;
        }

        public Task<bool> CheckFolderExistence(string folderName) => throw new System.NotImplementedException();

        public Task<bool> CreateFolder(string path) => throw new System.NotImplementedException();

        public Task<bool> DeleteSave(string path) => throw new System.NotImplementedException();

        public Task<bool> DownloadSaveData(GameInformationModel gameInformation) => throw new System.NotImplementedException();

        public Task<IEnumerable<(string name, string path)>> GetSavesList(GameInformationModel gameInformation) => throw new System.NotImplementedException();

        public Task<bool> UploadSaveData(GameInformationModel gameInformation, bool overwriteSave) => throw new System.NotImplementedException();
    }
}