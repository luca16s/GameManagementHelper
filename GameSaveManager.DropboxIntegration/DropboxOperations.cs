using Dropbox.Api;
using Dropbox.Api.Files;

using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameSaveManager.DropboxIntegration
{
    public class DropboxOperations : ICloudOperations
    {
        private readonly DropboxClient Client;
        private readonly IBackupStrategy BackupStrategy;

        public DropboxOperations(IBackupStrategy backupStrategy, DropboxClient dropboxClient)
        {
            BackupStrategy = backupStrategy;
            Client = dropboxClient;
        }

        public async Task<bool> DownloadSaveData(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return false;

            var fileList = await ListFolderContent(gameInformation.DefaultGameSaveFolder).ConfigureAwait(true);

            var fileFound = fileList.Entries.FirstOrDefault(save => save.IsFile);

            if (fileFound is null) return false;

            using var result = await Client.Files.DownloadAsync(gameInformation.DefaultGameSaveFolder + fileFound.Name).ConfigureAwait(true);

            using (var stream = File.OpenWrite($"{gameInformation.DefaultGameSaveFolder}\\{gameInformation.DefaultSaveName}"))
            {
                var dataToWrite = await result.GetContentAsByteArrayAsync().ConfigureAwait(true);
                stream.Write(dataToWrite, 0, dataToWrite.Length);
            }

            BackupStrategy.PrepareBackup(gameInformation);

            return true;
        }

        public async Task<bool> UploadSaveData(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return false;

            try
            {
                gameInformation.SaveBackupExtension = BackupStrategy.GetFileExtension();

                using var fileStream = BackupStrategy.GenerateBackup(gameInformation);

                var response = await Client
                    .Files
                    .UploadAsync(gameInformation.OnlineSaveFolder + gameInformation.DefaultSaveName, WriteMode.Add.Instance, body: fileStream)
                    .ConfigureAwait(true);

                return string.IsNullOrEmpty(response.ContentHash);
            }
            finally
            {
                if (FileSystemUtils.CheckFileExistence(FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName()))
                    FileSystemUtils.DeleteZipFile(FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName());
            }
        }

        public async Task<bool> CheckFolderExistence(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return false;

            var itemsList = await Client.Files.ListFolderAsync("").ConfigureAwait(true);

            bool hasFolder = CheckIfFolderExistsInList(folderName, itemsList);

            if (itemsList.HasMore)
            {
                itemsList = await Client.Files.ListFolderContinueAsync(folderName).ConfigureAwait(true);
                hasFolder = CheckIfFolderExistsInList(folderName, itemsList);
            }

            return hasFolder;
        }

        public async Task<bool> CreateFolder(string path)
        {
            var result = await Client.Files.CreateFolderV2Async(path?.TrimEnd('/')).ConfigureAwait(true);
            return string.IsNullOrEmpty(result.Metadata.Id);
        }

        private static bool CheckIfFolderExistsInList(string folderName,
                                                      ListFolderResult itemsList)
        {
            foreach (var item in itemsList.Entries.Where(x => x.IsFolder))
            {
                if (string.Equals(item.Name, folderName?.Trim('/'), StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        private async Task<ListFolderResult> ListFolderContent(string folderPath)
        {
            return await Client.Files.ListFolderAsync(folderPath).ConfigureAwait(true);
        }
    }
}