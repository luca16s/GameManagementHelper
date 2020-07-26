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
    }
}