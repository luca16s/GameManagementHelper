using Dropbox.Api;
using Dropbox.Api.Files;

using GameSaveManager.Core.Models;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSaveManager.DropboxIntegration
{
    public class DropboxConnection
    {
        public DropboxClient Client { get; private set; }

        public DropboxConnection(string token)
        {
            Client = new DropboxClient(token);
        }

        public static UserInfoModel GetAccountInfo(string name, string email)
        {
            return new UserInfoModel(name, email);
        }

        public async Task<bool> CheckFolderExistence(string folderName)
        {
            var itemsList = await Client.Files.ListFolderAsync(folderName).ConfigureAwait(true);

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
            var result = await Client.Files.CreateFolderV2Async(path).ConfigureAwait(true);
            return string.IsNullOrEmpty(result.Metadata.Id);
        }

        public async Task UploadGameSave(string filePath, string folder, string fileName)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(filePath));
            using var streamBody = new MemoryStream(Encoding.UTF8.GetBytes(filePath));
            var response = await Client
                .Files
                .UploadAsync($"{folder}/{fileName}", WriteMode.Overwrite.Instance, body: streamBody)
                .ConfigureAwait(true);
        }

        public static void DownloadGameSave()
        {

        }

        private bool CheckIfFolderExistsInList(string folderName, ListFolderResult itemsList)
        {
            foreach (var item in itemsList.Entries.Where(x => x.IsFolder))
            {
                return string.Equals(item.Name, folderName, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
    }
}