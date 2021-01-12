namespace GameSaveManager.DropboxApi
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Dropbox.Api;
    using Dropbox.Api.Files;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

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
            if (gameInformation == null)
                return false;

            ListFolderResult fileList = await ListFolderContent(gameInformation.OnlineSaveFolder.TrimEnd('/')).ConfigureAwait(true);

            Metadata fileFound = fileList.Entries.FirstOrDefault(save => save.IsFile
            && save.Name.Equals(gameInformation.BuildSaveName(), StringComparison.InvariantCultureIgnoreCase));

            if (fileFound is null)
                return false;

            using Dropbox.Api.Stone.IDownloadResponse<FileMetadata> result = await Client.Files.DownloadAsync(Path.Combine(gameInformation.OnlineSaveFolder, fileFound.Name)).ConfigureAwait(true);

            using (FileStream stream = File.OpenWrite(Path.Combine(FileSystemUtils.GetTempFolder(), fileFound.Name)))
            {
                byte[] dataToWrite = await result.GetContentAsByteArrayAsync().ConfigureAwait(true);
                stream.Write(dataToWrite, 0, dataToWrite.Length);
            }

            BackupStrategy.PrepareBackup(gameInformation);

            return true;
        }

        public async Task<bool> UploadSaveData(GameInformationModel gameInformation, bool overwriteSave)
        {
            if (gameInformation == null)
                return false;

            try
            {
                gameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

                using FileStream fileStream = BackupStrategy.GenerateBackup(gameInformation);

                WriteMode writeMode = overwriteSave ? WriteMode.Overwrite.Instance : WriteMode.Add.Instance;

                FileMetadata response = await Client
                    .Files
                    .UploadAsync(Path.Combine(gameInformation.OnlineSaveFolder, gameInformation.BuildSaveName()), writeMode, body: fileStream)
                    .ConfigureAwait(true);

                return string.IsNullOrEmpty(response.ContentHash);
            }
            finally
            {
                if (File.Exists(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName())))
                    _ = FileSystemUtils.DeleteCreatedFile(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName()));
            }
        }

        public async Task<bool> CheckFolderExistence(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return false;

            ListFolderResult itemsList = await Client.Files.ListFolderAsync("").ConfigureAwait(true);

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
            CreateFolderResult result = await Client.Files.CreateFolderV2Async(path?.TrimEnd('/')).ConfigureAwait(true);
            return string.IsNullOrEmpty(result.Metadata.Id);
        }

        private static bool CheckIfFolderExistsInList(string folderName, ListFolderResult itemsList)
        {
            foreach (Metadata item in itemsList.Entries.Where(x => x.IsFolder))
            {
                if (string.Equals(item.Name, folderName?.Trim('/'), StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        private async Task<ListFolderResult> ListFolderContent(string folderPath) => await Client.Files.ListFolderAsync(folderPath).ConfigureAwait(true);
    }
}