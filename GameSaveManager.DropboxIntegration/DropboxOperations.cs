using Dropbox.Api;
using Dropbox.Api.Files;

using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Services;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace GameSaveManager.DropboxIntegration
{
    public class DropboxOperations : ICloudOperations
    {
        private readonly DropboxClient Client;

        public DropboxOperations(DropboxClient dropboxClient)
        {
            Client = dropboxClient;
        }

        public async Task<bool> DownloadSaveData(string folderPath)
        {
            var fileList = await ListFolderContent(folderPath).ConfigureAwait(true);

            var fileFound = fileList.Entries.FirstOrDefault(save => save.IsFile);

            if (fileFound is null) return false;

            using var result = await Client.Files.DownloadAsync($"{folderPath}/{fileFound.Name}").ConfigureAwait(true);

            using (var stream = File.OpenWrite($"{Environment.CurrentDirectory}\\Ds.zip"))
            {
                var dataToWrite = await result.GetContentAsByteArrayAsync().ConfigureAwait(true);
                stream.Write(dataToWrite, 0, dataToWrite.Length);
            }

            return true;
        }

        public async Task<string> UploadSaveData(GameInformation gameInformation)
        {
            if (gameInformation == null)
            {
                return string.Empty;
            }

            var enviromentPath = FileSystemServices.GetAppDataFolderPath(folderName: gameInformation.FolderName);

            string zipPath = $"{Environment.CurrentDirectory}\\{gameInformation.SaveName}";

            ZipFile.CreateFromDirectory(enviromentPath, zipPath);

            using var streamBody = new FileStream(zipPath, FileMode.Open, FileAccess.Read);
            var response = await Client
                .Files
                .UploadAsync(gameInformation.OnlineDriveFolder + gameInformation.SaveName, WriteMode.Add.Instance, body: streamBody)
                .ConfigureAwait(true);

            File.Delete(zipPath);

            return response.ContentHash;
        }

        public async Task<bool> CheckFolderExistence(string folderName)
        {
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
            var result = await Client.Files.CreateFolderV2Async($"/{path}").ConfigureAwait(true);
            return string.IsNullOrEmpty(result.Metadata.Id);
        }

        private static bool CheckIfFolderExistsInList(string folderName,
                                                      ListFolderResult itemsList)
        {
            foreach (var item in itemsList.Entries.Where(x => x.IsFolder))
            {
                if (string.Equals(item.Name, folderName, StringComparison.InvariantCultureIgnoreCase))
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