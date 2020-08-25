using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System.IO;

namespace GameSaveManager.Core.Services
{
    public class BakBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => ".bak";

        public FileStream GenerateBackup(GameInformation gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.GetGameFolderLocationAppData(gameInformation.FolderName);

            var filesPathList = Directory.GetFiles(folder, gameInformation.GameSaveExtension, SearchOption.AllDirectories);

            for (int i = 0; i < filesPathList.Length; i++)
            {
                string path = filesPathList[i];
                File.Copy(path, gameInformation.ZipTempFolder);
            }

            return new FileStream(gameInformation.ZipTempFolder, FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformation gameInformation)
        {
            if (gameInformation == null) return;

            throw new System.NotImplementedException();
        }
    }
}